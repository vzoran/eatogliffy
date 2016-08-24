using System;
using EA;
using eatogliffy.gliffy.builder.core;
using NUnit.Framework;
using NSubstitute;
using eatogliffy.gliffy.exception;
using eatogliffy.gliffy.model;

namespace eatogliffyTest.gliffy.builder.core
{
    [TestFixture]
    public class MetadataBuilderUnitTest
    {
        [Test]
        public void TestMDBuilder()
        {
            MetadataBuilder metadataBuilder = new MetadataBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => metadataBuilder.build(), "Build() function cannot be executed without initialization");

            var mockedDiagram = Substitute.For<Diagram>();
            mockedDiagram.cx.Returns(1000);
            mockedDiagram.cy.Returns(1000);
            mockedDiagram.Name.Returns("Test");

            metadataBuilder.withEaDiagram(mockedDiagram);
            Assert.DoesNotThrow(() => metadataBuilder.build(), "Build() function cannot be failed with proper initialization");

            GliffyMetaData gliffyMetaData = metadataBuilder.getMetadata();
            Assert.NotNull(gliffyMetaData, "Metadata cannot be null after build.");

            Assert.AreNotEqual(0, gliffyMetaData.lastSerialized, "Lastserialized cannot be 0");
            Assert.AreEqual("Confluence", gliffyMetaData.analyticsProduct, "AnalyticsProduct cannot be empty");
            Assert.AreEqual("default", gliffyMetaData.loadPosition, "AnalyticsProduct cannot be empty");
            Assert.AreEqual("Test", gliffyMetaData.title, "title cannot be empty");
            Assert.NotNull(gliffyMetaData.libraries, "Libraries cannot be null");
            Assert.AreNotEqual(0, gliffyMetaData.libraries.Count, "Libraries cannot be empty");
        }
    }
}
