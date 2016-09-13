using EaToGliffy.Gliffy.Builder.Tools;
using NUnit.Framework;
using NSubstitute;
using System;
using EA;
using eatogliffyTest.gliffy.builder.Common;

namespace eatogliffyTest.gliffy.builder.tools
{
    [TestFixture]
    public class BuilderToolsTest
    {
        [Test]
        public void TestConverters()
        {

            Assert.AreEqual("#000000", BuilderTools.HexConverter(0), "HexConvert error");
            Assert.AreEqual("#FFFFFF", BuilderTools.HexConverter(Int32.MaxValue), "HexConvert error");

            Assert.AreEqual("#123456", BuilderTools.HexConverter(Int32.MaxValue, "#123456"), "HexConvert error - defaults");
            Assert.AreNotEqual("#123456", BuilderTools.HexConverter(12, "#123456"), "HexConvert error - not defaults");
        }

        [Test]
        public void TestDiagramObjects()
        {
            var mockedDiagram = Substitute.For<Diagram>();

            Assert.IsNull(BuilderTools.GetDiagramObjectById(null, "0001"), "Null check error 01");
            Assert.IsNull(BuilderTools.GetDiagramObjectById(null, ""), "Null check error 02");
            Assert.IsNull(BuilderTools.GetDiagramObjectById(mockedDiagram, ""), "Null check error 03");
            Assert.IsNull(BuilderTools.GetDiagramObjectById(null, null), "Null check error 04");
            Assert.IsNull(BuilderTools.GetDiagramObjectById(mockedDiagram, null), "Null check error 05");

            DiagramObjectList mockedList = new DiagramObjectList();
            mockedDiagram.DiagramObjects.Returns(mockedList);

            Assert.IsNull(BuilderTools.GetDiagramObjectById(mockedDiagram, "0001"), "Empty check error 01");
        }
    }
}