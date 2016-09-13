using System;
using EA;
using EaToGliffy.Gliffy.Builder.Core;
using NUnit.Framework;
using NSubstitute;
using EaToGliffy.Gliffy.Exception;
using EaToGliffy.Gliffy.Model;

namespace eatogliffyTest.gliffy.builder.core
{
    [TestFixture]
    public class MetadataBuilderUnitTest
    {
        [Test]
        public void TestMDBuilder()
        {
            MetadataBuilder metadataBuilder = new MetadataBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => metadataBuilder.Build(), "Build() function cannot be executed without initialization");

            var mockedDiagram = Substitute.For<Diagram>();
            mockedDiagram.cx.Returns(1000);
            mockedDiagram.cy.Returns(1000);
            mockedDiagram.Name.Returns("Test");

            metadataBuilder.WithEaDiagram(mockedDiagram);
            Assert.DoesNotThrow(() => metadataBuilder.Build(), "Build() function cannot be failed with proper initialization");

            GliffyMetaData gliffyMetaData = metadataBuilder.GetMetadata();
            Assert.NotNull(gliffyMetaData, "Metadata cannot be null after Build.");

            Assert.AreNotEqual(0, gliffyMetaData.LastSerialized, "Lastserialized cannot be 0");
            Assert.AreEqual("Confluence", gliffyMetaData.AnalyticsProduct, "AnalyticsProduct cannot be empty");
            Assert.AreEqual("default", gliffyMetaData.LoadPosition, "AnalyticsProduct cannot be empty");
            Assert.AreEqual("Test", gliffyMetaData.Title, "title cannot be empty");
            Assert.NotNull(gliffyMetaData.Libraries, "Libraries cannot be null");
            Assert.AreNotEqual(0, gliffyMetaData.Libraries.Count, "Libraries cannot be empty");
        }
    }
}
