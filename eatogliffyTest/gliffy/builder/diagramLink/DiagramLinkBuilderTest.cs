using System;

using EaToGliffy.Gliffy.Builder.DiagramLinks;
using NUnit.Framework;
using EaToGliffy.Gliffy.Exception;
using EA;
using NSubstitute;
using EaToGliffy.Gliffy.Model;

namespace eatogliffyTest.gliffy.builder.diagramLink
{
    [TestFixture]
    public class DiagramLinkBuilderUnitTest
    {
        [Test]
        public void TestNullSettings()
        {
            DependecyBuilder dependencyBuilder = new DependecyBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => dependencyBuilder.Build(), "Build() function cannot be executed without initialization");

            var mockedConnector = Substitute.For<Connector>();

            dependencyBuilder.WithEaConnector(mockedConnector);
            Assert.Catch<InvalidBuilderSetupException>(() => dependencyBuilder.Build(), "Build() function cannot be executed without proper initialization: EAObject: yes");

            var mockedDiagramLink = Substitute.For<DiagramLink>();

            dependencyBuilder.WithEaLink(mockedDiagramLink);
            Assert.Catch<InvalidBuilderSetupException>(() => dependencyBuilder.Build(), "Build() function cannot be executed without proper initialization: EAObject: yes, EAElement: yes");

            var mockedRepository = Substitute.For<Repository>();
            dependencyBuilder.WithEaRepository(mockedRepository);
            Assert.Catch<InvalidBuilderSetupException>(() => dependencyBuilder.Build(), "Build() function cannot be executed without proper initialization: EAObject: yes, EAElement: yes, EARepository: yes");
            /*
            dependencyBuilder.WithLayer("LayerId");
            Assert.DoesNotThrow(() => dependencyBuilder.Build(), "Build() function cannot be failed with proper initialization");

            GliffyObject gliffyObject = dependencyBuilder.GetObject();
            Assert.NotNull(gliffyObject, "Generated component cannot be null");
            Assert.NotNull(gliffyObject.graphic, "Generated graphic cannot be null");
            Assert.AreEqual(gliffyObject.uid, "com.gliffy.shape.uml.uml_v2.class.dependency", "Invalid object uid");
            */
        }
    }
}
