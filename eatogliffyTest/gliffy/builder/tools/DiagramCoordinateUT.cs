using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eatogliffy.gliffy.builder.tools;

namespace eatogliffyTest
{
    [TestClass]
    public class DiagramCoordinateUT
    {
        [TestMethod]
        public void TestDefaultConstructor()
        {
            DiagramCoordinate diagramCoordinate = new DiagramCoordinate();
            Assert.AreEqual(0, diagramCoordinate.PointX, 0f, "X coordinate must be 0 in default constructor");
            Assert.AreEqual(0, diagramCoordinate.PointY, 0f, "Y coordinate must be 0 in default constructor");
        }

        [TestMethod]
        public void TestNormalConstructor()
        {
            const int pointX = 10;
            const int pointY = 20;

            DiagramCoordinate diagramCoordinate = new DiagramCoordinate(pointX, pointY);
            Assert.AreEqual(pointX, diagramCoordinate.PointX, 0f, String.Format("X coordinate must be {0} in normal constructor", pointX));
            Assert.AreEqual(pointY, diagramCoordinate.PointY, 0f, String.Format("Y coordinate must be {0} in normal constructor", pointY));
        }


    }
}
