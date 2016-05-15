using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eatogliffy.gliffy.builder.tools;

namespace eatogliffyTest
{
    [TestClass]
    public class DiagramCoordinateUnitTest
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

        [TestMethod]
        public void TestNormalization()
        {
            const int pointX1 = 10;
            const int pointY1 = 20;
            const int pointX2 = -10;
            const int pointY2 = -20;

            DiagramCoordinate diagramCoordinate = new DiagramCoordinate();

            diagramCoordinate.PointX = pointX1;
            diagramCoordinate.PointY = pointY1;
            Assert.AreEqual(pointX1, diagramCoordinate.NormalizedPointX, 0f, String.Format("X coordinate must be {0}", pointX1));
            Assert.AreEqual(pointY1, diagramCoordinate.NormalizedPointY, 0f, String.Format("Y coordinate must be {0}", pointY1));

            diagramCoordinate.PointX = pointX2;
            diagramCoordinate.PointY = pointY2;
            Assert.AreEqual(pointX2, diagramCoordinate.NormalizedPointX, 0f, String.Format("X coordinate must be {0}", pointX2));
            Assert.AreEqual(-1 * pointY2, diagramCoordinate.NormalizedPointY, 0f, String.Format("Y coordinate must be {0}", -1 * pointY2));
        }
    }
}
