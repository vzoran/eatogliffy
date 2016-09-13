using System;
using EA;
using EaToGliffy.Gliffy.Builder.Core;
using NUnit.Framework;
using NSubstitute;
using EaToGliffy.Gliffy.Exception;

namespace eatogliffyTest.gliffy.builder.core
{
    [TestFixture]
    public class StageBuilderUnitTest
    {
        [Test]
        public void TestSTGBuilder()
        {
            StageBuilder stageBuilder = new StageBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => stageBuilder.Build(), "Build() function cannot be executed without initialization");

            var mockedDiagram = Substitute.For<Diagram>();
            mockedDiagram.cx.Returns(1000);
            mockedDiagram.cy.Returns(1000);
            mockedDiagram.Name.Returns("Test");

            stageBuilder.WithEaDiagram(mockedDiagram);
            Assert.Catch<InvalidBuilderSetupException>(() => stageBuilder.Build(), "Build() function cannot be executed without initialization");

            StageBuilder stageBuilder2 = new StageBuilder();
            var mockedRepository = Substitute.For<Repository>();
            stageBuilder2.WithEaRepository(mockedRepository);
            Assert.Catch<InvalidBuilderSetupException>(() => stageBuilder2.Build(), "Build() function cannot be executed without initialization");

            
        }
    }
}
