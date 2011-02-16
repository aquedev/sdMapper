using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Lemon.SitecoreLib.DataAccess.ValueResolvers;

namespace Lemon.SitecoreLib.Test.DataAccess.ValueResolvers
{

    [TestFixture]
    public class EnumValueResolverTests
    {
        private static EnumValueResolver GetResolver()
        {
            return new EnumValueResolver();
        }

        [Test]
        public void ResolveEntityPropertyValue_WhenPassedAStringThatMatchAnEnumValue_ReturnsCorrectEnumValue()
        {
            var resolver = GetResolver();
            object result = resolver.ResolveEntityPropertyValue(EnumValueResolverTestEnum.Value1.ToString(), typeof(EnumValueResolverTestEnum));
            Assert.AreEqual(EnumValueResolverTestEnum.Value1, result);
        }
        

        [Test]
        public void ResolveEntityPropertyValue_WhenPassedAStringThatDoesNotMatchAnEnumValue_ReturnsFirstValue()
        {
            var resolver = GetResolver();
            object result = resolver.ResolveEntityPropertyValue("RandomStringThatDoesNotMatchAnyEnumValues", typeof(EnumValueResolverTestEnum));
            Assert.AreEqual(EnumValueResolverTestEnum.Value1, result);
        }

        [Test]
        public void CanResolver_WithEnum_ReturnsTrue()
        {
            var resolver = GetResolver();
            Assert.IsTrue(resolver.CanResolve(typeof(EnumValueResolverTestEnum)));
        }

        [Test]
        public void CanResolver_WithNonEnumType_ReturnsFalse()
        {
            var resolver = GetResolver();
            Assert.IsFalse(resolver.CanResolve(typeof(object)));
        }

        [Test]
        public void ResolveItemFieldValue_ReturnsTheStringRepresentationOfTheEnum()
        {
            var resolver = GetResolver();
            var result = resolver.ResolveItemFieldValue(EnumValueResolverTestEnum.Value2);
            Assert.AreEqual(EnumValueResolverTestEnum.Value2.ToString(), result);
        }

    }

    public enum EnumValueResolverTestEnum
    {
        Value1,
        Value2,
        Value3
    }
}
