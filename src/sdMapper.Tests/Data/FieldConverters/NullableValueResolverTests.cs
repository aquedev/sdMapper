using System;
using Lemon.SitecoreLib.DataAccess.ValueResolvers;
using NUnit.Framework;

namespace Lemon.SitecoreLib.Test.DataAccess.ValueResolvers
{
    [TestFixture]
    public class NullableValueResolverTests
    {
        private static NullableResolver m_resolver;

        [SetUp]
        public void SetUp()
        {
            m_resolver = new NullableResolver ();
        }

        [Test]
        public void Test_Valid_Date_Resolves_Successfully()
        {
            string dateValue = DateTime.Today.ToString(GetIsoFormat());
            var result = m_resolver.ResolveEntityPropertyValue(dateValue, typeof(DateTime?)) as DateTime?;
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(DateTime.Today, result.Value);

        }

        private static string GetIsoFormat()
        {
            return @"yyyyMMdd\THHmmss";
        }

        [Test]
        public void Test_Invalid_Date_Does_Not_Resolve()
        {
            const string dateValue = "Invalid data";
            var result = m_resolver.ResolveEntityPropertyValue(dateValue, typeof(DateTime?)) as DateTime?;
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void Test_Can_Resolve_Nullable_DateTime()
        {
            Assert.IsTrue(m_resolver.CanResolve(typeof(DateTime?)));
        }

        [Test]
        public void Test_Can_Not_Resolve_DateTime()
        {
            Assert.IsTrue(m_resolver.CanResolve(typeof(DateTime?)));
        }

        [Test]
        public void Show_DateTime_Value()
        {
            DateTime? dt = DateTime.Now;
            Assert.AreEqual(dt.Value.ToString(GetIsoFormat()), m_resolver.ResolveItemFieldValue(dt));
        }


        [Test]
        public void Can_Resolve_Nullable_Ints()
        {
            Type type = typeof(int?);
            Assert.IsTrue(m_resolver.CanResolve(type));
        }

        [Test]
        public void Resolves_Nullable_Ints()
        {
            const string rawValue = "4321";
            var result = m_resolver.ResolveEntityPropertyValue(rawValue, typeof(int?)) as int?;
            Assert.IsTrue(result.HasValue);
            Assert.IsTrue(result.Value.ToString().Equals(rawValue));
        }

        [Test]
        public void Returns_Empty_Nullable_On_Resolve_Failure()
        {
            const string rawValue = "yes";
            var result = (int?)m_resolver.ResolveEntityPropertyValue(rawValue, typeof(int?));
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ResolveEntityPropertyValue_WhenPassedAStringThatMatchAnEnumValue_ReturnsCorrectEnumValue()
        {
            object result = m_resolver.ResolveEntityPropertyValue(EnumValueResolverTestEnum.Value1.ToString(), typeof(EnumValueResolverTestEnum?));
            Assert.AreEqual(EnumValueResolverTestEnum.Value1, result);
        }


        [Test]
        public void ResolveEntityPropertyValue_WhenPassedAStringThatDoesNotMatchAnEnumValue_ReturnsFirstValue()
        {
            var result = m_resolver.ResolveEntityPropertyValue("RandomStringThatDoesNotMatchAnyEnumValues", typeof(EnumValueResolverTestEnum?)) as EnumValueResolverTestEnum?;
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void CanResolver_WithNullableEnum_ReturnsTrue()
        {
            Assert.IsTrue(m_resolver.CanResolve(typeof(EnumValueResolverTestEnum?)));
        }

        [Test]
        public void CanResolver_WithNonNullableEnumType_ReturnsFalse()
        {
            Assert.IsFalse(m_resolver.CanResolve(typeof(EnumValueResolverTestEnum)));
        }

        [Test]
        public void ResolveItemFieldValue_ReturnsTheStringRepresentationOfTheEnum()
        {
            EnumValueResolverTestEnum? rawValue = EnumValueResolverTestEnum.Value2;
            var result = m_resolver.ResolveItemFieldValue(rawValue);
            Assert.AreEqual(EnumValueResolverTestEnum.Value2.ToString(), result);
        }
    }
}