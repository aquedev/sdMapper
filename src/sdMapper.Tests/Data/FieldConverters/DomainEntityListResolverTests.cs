using System.Collections.Generic;
using NUnit.Framework;
using Lemon.SitecoreLib.DataAccess.ValueResolvers;
using System;
using Lemon.Common;
using Moq;
using Lemon.SitecoreLib.DataAccess;
using Lemon.Domain;
using System.Collections;
using Lemon.DataAccess;

namespace Lemon.SitecoreLib.Test.DataAccess.ValueResolvers
{
    [TestFixture]
    public class DomainEntityListResolverTests
    {
        Mock<IDomainEntityRepository> m_repositoryMock;
        Mock<ISitecoreDataAccessSettings> m_settingsMock;

        [SetUp]
        public void Setup()
        {
            m_repositoryMock = new Mock<IDomainEntityRepository>();
            m_settingsMock =  new Mock<ISitecoreDataAccessSettings>();
            m_settingsMock.SetupGet(settings => settings.ValueDelimiter).Returns("|");
        }

        private DomainEntityListResolver GetResolver()
        {
            return new DomainEntityListResolver(m_repositoryMock.Object, m_settingsMock.Object);
        }

        private void SetupRepositoryMockToReturnListOfTestDomainEntity()
        {
            m_repositoryMock.Setup(repo => repo.GetEntities(It.IsAny<IEnumerable<Guid>>(), It.IsAny<IMap>())).Returns(new List<ISitecoreDomainEntity>() {
            new TestDomainEnitity(),
            new TestDomainEnitity(),
            new TestDomainEnitity()
            }).Verifiable();
        }

        [Test]
        public void CanResolve_WithNotGenericType_ReturnsFalse()
        {
            var resolver = GetResolver();
            Type testType = typeof(string);

            Assert.False(resolver.CanResolve(testType));
        }

        [Test]
        public void CanResolve_WithGenericTypeThatsNotAList_ReturnsFalse()
        {
            var resolver = GetResolver();
            Type testType = typeof(Nullable<int>);

            Assert.False(resolver.CanResolve(testType));
        }

        [Test]
        public void CanResolve_WithGenericIListOfInt_ReturnsFalse()
        {
            var resolver = GetResolver();
            Type testType = typeof(IList<int>);

            Assert.False(resolver.CanResolve(testType));
        }

        [Test]
        public void CanResolve_WithGenericIListOfSitecoreDomainEntity_ReturnsTrue()
        {
            var resolver = GetResolver();
            Type testType = typeof(IList<TestDomainEnitity>);

            Assert.True(resolver.CanResolve(testType));
        }

        [Test]
        public void ResolveEntityPropertyValue_WithEmptyRawValue_ReturnEmptyList()
        {
            var resolver = GetResolver();
            Type propertyType = typeof(IList<TestDomainEnitity>);
            Type expectedType = typeof(List<TestDomainEnitity>);

            object result = resolver.ResolveEntityPropertyValue(String.Empty, propertyType);
            Assert.IsInstanceOf(expectedType, result);
        }

        [Test]
        public void ResolveEntityPropertyValue_WithListOfGuids_ReturnsLazyList()
        {
            var resolver = GetResolver();
            Type propertyType = typeof(IList<TestDomainEnitity>);
            Type expectedType = typeof(LazyList<TestDomainEnitity>);
            string rawValue = Guid.NewGuid() + "|" + Guid.NewGuid();
            SetupRepositoryMockToReturnListOfTestDomainEntity();

            object result = resolver.ResolveEntityPropertyValue(rawValue, propertyType);

            Assert.IsInstanceOf(expectedType, result);
        }

        [Test]
        public void ResolveEntityPropertyValue_WithListOfGuids_NoEntitiesAreResolverBeforeTheReturnedListHasBeenAccessed()
        {
            var resolver = GetResolver();
            Type propertyType = typeof(IList<TestDomainEnitity>);
            Type expectedType = typeof(LazyList<TestDomainEnitity>);
            string rawValue = Guid.NewGuid() + "|" + Guid.NewGuid();
            SetupRepositoryMockToReturnListOfTestDomainEntity();

            object result = resolver.ResolveEntityPropertyValue(rawValue, propertyType);

            m_repositoryMock.Verify(repo => repo.GetEntities(It.IsAny<IEnumerable<Guid>>(), It.IsAny<IMap>()), Times.Never(), "It's a lazy list so the repository shouldn't be called before the list has been assesed");
        }

        [Test]
        public void ResolveEntityPropertyValue_WithListOfGuids_TheListEntitiesAreResolvedWhenTheListWasAccessed()
        {
            var resolver = GetResolver();
            Type propertyType = typeof(IList<TestDomainEnitity>);
            Type expectedType = typeof(LazyList<TestDomainEnitity>);
            string rawValue = Guid.NewGuid() + "|" + Guid.NewGuid();
            SetupRepositoryMockToReturnListOfTestDomainEntity();

            var result = resolver.ResolveEntityPropertyValue(rawValue, propertyType) as IList<TestDomainEnitity>;

            int count = result.Count;
            m_repositoryMock.Verify(repo => repo.GetEntities(It.IsAny<IEnumerable<Guid>>(), It.IsAny<IMap>()), Times.AtLeastOnce(), "The repository should have been accessed to resovle the required entities");
        }

        [Test]
        public void ResolveEntityPropertyValue_WithListOfGuids_TheListContainsThreeEntities()
        {
            var resolver = GetResolver();
            Type propertyType = typeof(IList<TestDomainEnitity>);
            Type expectedType = typeof(LazyList<TestDomainEnitity>);
            string rawValue = Guid.NewGuid() + "|" + Guid.NewGuid();
            SetupRepositoryMockToReturnListOfTestDomainEntity();

            var result = resolver.ResolveEntityPropertyValue(rawValue, propertyType) as IList<TestDomainEnitity>;

            Assert.AreEqual(3, result.Count);

        }
    }
}
