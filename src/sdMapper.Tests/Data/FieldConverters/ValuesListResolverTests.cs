using System;
using System.Collections.Generic;
using NUnit.Framework;
using Lemon.SitecoreLib.DataAccess.ValueResolvers;
using Lemon.Domain;
using System.Collections;
using Lemon.SitecoreLib.DataAccess;
using Lemon.DataAccess;
using Moq;

namespace Lemon.SitecoreLib.Test.DataAccess.ValueResolvers
{
    [TestFixture]
    public class ValuesListResolverTests
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var settings = new Mock<ISitecoreDataAccessSettings>();
            settings.SetupGet(setting => setting.ValueDelimiter).Returns("|");
            SitecoreDataAccess.Initialize(settings.Object); 
        }

        [Test]
        public void CanResolve_WithGenericList_ReturnsTrue()
        {
            var resolver = GetResolver();
            Assert.IsTrue(resolver.CanResolve(typeof(IList<string>)));
        }

        [Test]
        public void CanResolve_WithGenericListOfISitecoreDomainEnititys_ReturnsFalse()
        {
            var resolver = GetResolver();
            Assert.IsFalse(resolver.CanResolve(typeof(List<ISitecoreDomainEntity>)));
        }

        [Test]
        public void CanResolve_WithGenericListOfDomainEnitiesThatImplementISitecoreDomainEntity_ReturnsFalse()
        {
            var resolver = GetResolver();
            Assert.IsFalse(resolver.CanResolve(typeof(List<TestDomainEnitity>)));
        }

        [Test]
        public void CanResolve_WithNonGenericType_ReturnsFalse()
        {
            var resolver = GetResolver();
            Assert.IsFalse(resolver.CanResolve(typeof(string)));
        }

        [Test]
        public void ResolveEntityPropertyValue_WithEmtpyString_ReturnAnEmptyIList()
        {
            var resolver = GetResolver();
            var result = resolver.ResolveEntityPropertyValue("", typeof(IList<int>)) as IList<int>;
            Assert.IsInstanceOf(typeof(IList<int>), result);
            Assert.IsEmpty(result as ICollection);
        }

        [Test]
        public void ResolveEntityPropertyValue_WithPipeSeparatedIntList_ReturnCorrectList()
        {
            var resolver = GetResolver();
            var result = (IList<int>)resolver.ResolveEntityPropertyValue("10|20", typeof(IList<int>));
            Assert.That(result.Count == 2, "Wrong number of list items");
            Assert.That(result[0] == 10, "Wrong first value, expected: 10, actual: " + result[0]);
        }

        [Test]
        public void ResolveEntityPropertyValue_WithListTypeThatHasNoValueResolver_ThrowsException()
        {
            var resolver = GetResolver();
        	string expectedMessage = "Cannon find resolver for System.Object and cannot process the list";
        	Assert.Throws<InvalidOperationException>(
        		() => resolver.ResolveEntityPropertyValue("10|20", typeof (IList<object>)), expectedMessage);
        }

        [Test]
        public void ResolveItemFieldValue_WithListTypeThatHasNoValueResolver_ThrowsException()
        {
            var resolver = GetResolver();
			string expectedMessage = "Cannot find resolver for System.Object and cannot process the list";
			Assert.Throws<InvalidOperationException>(
				() => resolver.ResolveItemFieldValue(new List<object> { new object()}), expectedMessage);
        }

        [Test]
        public void ResolveItemFieldValue_WithListOfInt_ReturnsPipeSeparatedListOfIntegers()
        {
            var resolver = GetResolver();
            var result = resolver.ResolveItemFieldValue(new List<int> { 10, 20 });
            Assert.AreEqual("10|20", result.ToString());
        }

        [Test]
        public void ResolveItemFieldValue_WithEmptyList_ReturnsEmptyString()
        {
            var resolver = GetResolver();
            var result = resolver.ResolveItemFieldValue(new List<int>());
            Assert.AreEqual(String.Empty, result);
        }

        [Test]
        public void ResolveItemFieldValue_WithNullList_ReturnsEmptyString()
        {
            var resolver = GetResolver();
            var result = resolver.ResolveItemFieldValue(null);
            Assert.AreEqual(String.Empty, result);
        }


        private static ValuesListResolver GetResolver()
        {
            return new ValuesListResolver(new List<IValueResolver> { new ConvertibleValueResolver() });
        }
    }

    public class TestDomainEnitity : ISitecoreDomainEntity
    {
        public Guid Id
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

    }
}
