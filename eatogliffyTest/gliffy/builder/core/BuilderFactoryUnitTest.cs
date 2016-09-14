using System;
using EA;
using EaToGliffy.Gliffy.Builder.Core;
using NUnit.Framework;
using NSubstitute;
using EaToGliffy.Gliffy.Builder.DiagramLinks;
using EaToGliffy.Gliffy.Builder.DiagramObjects;

namespace eatogliffyTest.gliffy.builder.core
{
    [TestFixture]
    public class BuilderFactoryUnitTest
    {
        [Test]
        public void TestLinkBuilders()
        {
            Assert.IsNull(BuilderFactory.GetLinkBuilder("aadas"), "Invalid link builder should be null");

            LinkHelper(typeof(DependecyBuilder), "Dependency");
            LinkHelper(typeof(AssociationBuilder), "Association");
            LinkHelper(typeof(AggregationBuilder), "Aggregation");
            LinkHelper(typeof(CompositionBuilder), "Instantiation");
        }

        [Test]
        public void TestObjectBuilders()
        {
            Assert.IsNull(BuilderFactory.GetObjectBuilder("aadas"), "Invalid object builder should be null");

            ObjectHelper(typeof(BoundaryBuilder), "Boundary");
            ObjectHelper(typeof(ComponentBuilder), "Component");
        }

        private void LinkHelper(Type targetType, string typeName)
        {
            LinkBuilder linkBuilder = BuilderFactory.GetLinkBuilder(typeName);
            Assert.IsNotNull(linkBuilder, "{0} has to be handled", typeName);
            Assert.AreEqual(linkBuilder, BuilderFactory.GetLinkBuilder(typeName), "Should not create new {0}", targetType.ToString());
            Assert.IsInstanceOf(targetType, linkBuilder, "Invalid type for {0}", typeName);
        }

        private void ObjectHelper(Type targetType, string typeName)
        {
            ObjectBuilder objectBuilder = BuilderFactory.GetObjectBuilder(typeName);
            Assert.IsNotNull(objectBuilder, "{0} has to be handled", typeName);
            Assert.AreEqual(objectBuilder, BuilderFactory.GetObjectBuilder(typeName), "Should not create new {0}", targetType.ToString());
            Assert.IsInstanceOf(targetType, objectBuilder, "Invalid type for {0}", typeName);
        }
    }
}
