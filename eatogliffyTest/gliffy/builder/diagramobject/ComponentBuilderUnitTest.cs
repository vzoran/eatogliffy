using System;

using eatogliffy.gliffy.builder.diagramobject;
using NUnit.Framework;
using eatogliffy.gliffy.exception;
using EA;
using NSubstitute;
using eatogliffy.gliffy.model;

namespace eatogliffyTest.gliffy.builder.diagramobject
{
    [TestFixture]
    public class ComponentBuilderUnitTest
    {
        [Test]
        public void TestNullSettings()
        {
            ComponentBuilder componentBuilder = new ComponentBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.buildAsParent(), "BuildAsParent() function cannot be executed without initialization");
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.buildAsChild(), "BuildAsChild function cannot be executed without initialization");

            var mockedDiagramObject = Substitute.For<DiagramObject>();
            mockedDiagramObject.right.Returns(200);
            mockedDiagramObject.left.Returns(100);
            mockedDiagramObject.top.Returns(-100);
            mockedDiagramObject.bottom.Returns(-200);
            mockedDiagramObject.BackgroundColor.Returns(0);
            mockedDiagramObject.BorderColor.Returns(0);
            mockedDiagramObject.BorderLineWidth.Returns(2);
            
            componentBuilder.withEaObject(mockedDiagramObject);
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.buildAsParent(), "BuildAsParent() function cannot be executed without proper initialization: EAObject: yes");
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.buildAsChild(), "BuildAsChild function cannot be executed without proper initialization: EAObject: yes");

            var mockedElement = Substitute.For<Element>();
            mockedElement.Name.Returns("Test element");
            mockedElement.ElementGUID.Returns("d1b10e65-27a0-4833-80ba-4238d1e16b6d");
            
            componentBuilder.withEaElement(mockedElement);
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.buildAsParent(), "BuildAsParent() function cannot be executed without proper initialization: EAObject: yes, EAElement: yes");
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.buildAsChild(), "BuildAsChild function cannot be executed without proper initialization: EAObject: yes, EAElement: yes");

            componentBuilder.withLayer("LayerId");
            Assert.DoesNotThrow(() => componentBuilder.buildAsParent(), "BuildAsParent() function cannot be failed with proper initialization");
            Assert.DoesNotThrow(() => componentBuilder.buildAsChild(), "BuildAsChild() function cannot be failed with proper initialization");

            GliffyObject gliffyObject = componentBuilder.getObject();
            Assert.NotNull(gliffyObject, "Generated component cannot be null");
            Assert.NotNull(gliffyObject.graphic, "Generated graphic cannot be null");
            Assert.AreEqual(gliffyObject.uid, "com.gliffy.shape.uml.uml_v2.component.component1", "Invalid object uid");
        }
    }
}
