using System;

using EaToGliffy.Gliffy.Builder.DiagramObjects;
using NUnit.Framework;
using EaToGliffy.Gliffy.Exception;
using EA;
using NSubstitute;
using EaToGliffy.Gliffy.Model;

namespace eatogliffyTest.gliffy.builder.diagramobject
{
    [TestFixture]
    public class ComponentBuilderUnitTest
    {
        [Test]
        public void TestNullSettings()
        {
            ComponentBuilder componentBuilder = new ComponentBuilder();
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.BuildAsParent(), "BuildAsParent() function cannot be executed without initialization");
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.BuildAsChild(), "BuildAsChild function cannot be executed without initialization");

            var mockedDiagramObject = Substitute.For<DiagramObject>();
            mockedDiagramObject.right.Returns(200);
            mockedDiagramObject.left.Returns(100);
            mockedDiagramObject.top.Returns(-100);
            mockedDiagramObject.bottom.Returns(-200);
            mockedDiagramObject.BackgroundColor.Returns(0);
            mockedDiagramObject.BorderColor.Returns(0);
            mockedDiagramObject.BorderLineWidth.Returns(2);
            
            componentBuilder.WithEaObject(mockedDiagramObject);
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.BuildAsParent(), "BuildAsParent() function cannot be executed without proper initialization: EAObject: yes");
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.BuildAsChild(), "BuildAsChild function cannot be executed without proper initialization: EAObject: yes");

            var mockedElement = Substitute.For<Element>();
            mockedElement.Name.Returns("Test element");
            mockedElement.ElementGUID.Returns("d1b10e65-27a0-4833-80ba-4238d1e16b6d");
            
            componentBuilder.WithEaElement(mockedElement);
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.BuildAsParent(), "BuildAsParent() function cannot be executed without proper initialization: EAObject: yes, EAElement: yes");
            Assert.Catch<InvalidBuilderSetupException>(() => componentBuilder.BuildAsChild(), "BuildAsChild function cannot be executed without proper initialization: EAObject: yes, EAElement: yes");

            componentBuilder.WithLayer("LayerId");
            Assert.DoesNotThrow(() => componentBuilder.BuildAsParent(), "BuildAsParent() function cannot be failed with proper initialization");
            Assert.DoesNotThrow(() => componentBuilder.BuildAsChild(), "BuildAsChild() function cannot be failed with proper initialization");

            GliffyObject gliffyObject = componentBuilder.GetObject();
            Assert.NotNull(gliffyObject, "Generated component cannot be null");
            Assert.NotNull(gliffyObject.Graphic, "Generated graphic cannot be null");
            Assert.AreEqual(gliffyObject.Uid, "com.gliffy.shape.uml.uml_v2.component.component1", "Invalid object uid");
        }
    }
}
