using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.tools
{
    /// <summary>
    /// Description of a point in diagram space
    /// </summary>
    public class DiagramCoordinate
    {
        public int PointX { get; set; }
        public int PointY { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiagramCoordinate()
        {
            PointX = 0;
            PointY = 0;
        }

        /// <summary>
        /// Constructor and intialize members
        /// </summary>
        /// <param name="pointX">X coordinate value</param>
        /// <param name="pointY">Y coordinate value</param>
        public DiagramCoordinate(int pointX, int pointY)
        {
            PointX = pointX;
            PointY = pointY;
        }

        /// <summary>
        /// Coordinate X in Gliffy's coordinate system
        /// </summary>
        public int NormalizedPointX
        {
            get
            {
                return PointX;
            }
        }

        /// <summary>
        /// Coordinate Y in Gliffy's coordinate system
        /// </summary>
        public int NormalizedPointY
        {
            get
            {
                return Math.Abs(PointY);
            }
        }
    }
}
