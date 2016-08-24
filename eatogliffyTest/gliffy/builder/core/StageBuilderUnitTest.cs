using System;
using EA;
using eatogliffy.gliffy.builder.core;
using NUnit.Framework;
using NSubstitute;
using eatogliffy.gliffy.exception;

namespace eatogliffyTest.gliffy.builder.core
{
    [TestFixture]
    public class StageBuilderUnitTest
    {
        [Test]
        public void TestSTGBuilder()
        {
            StageBuilder stageBuilder = new StageBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => stageBuilder.build(), "Build() function cannot be executed without initialization");

            var mockedDiagram = Substitute.For<Diagram>();
            mockedDiagram.cx.Returns(1000);
            mockedDiagram.cy.Returns(1000);
            mockedDiagram.Name.Returns("Test");

            stageBuilder.withEaDiagram(mockedDiagram);
            Assert.Catch<InvalidBuilderSetupException>(() => stageBuilder.build(), "Build() function cannot be executed without initialization");

            StageBuilder stageBuilder2 = new StageBuilder();
            var mockedRepository = Substitute.For<Repository>();
            stageBuilder2.withEaRepository(mockedRepository);
            Assert.Catch<InvalidBuilderSetupException>(() => stageBuilder2.build(), "Build() function cannot be executed without initialization");

            
        }
    }
}
