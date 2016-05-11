using EA;
using eatogliffy.gliffy.builder.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.graphics.path
{
    /// <summary>
    /// HElper class to support tree-like line paths
    /// </summary>
    public class TreePathBuilder : PathBuilder
    {
        /// <summary>
        /// Construct line path from start to end
        /// </summary>
        protected override void buildSegments()
        {
            this.segments.Add(calculatePosition(linkInfo.Edge, linkInfo.Start, startObject));
            this.segments.AddRange(getControlPath());
            this.segments.Add(getEndPoint());
        }

        /// <summary>
        /// Translate diagram link's path string to a list of int arrays
        /// </summary>
        /// <returns>Generated list of coordinates</returns>
        private List<int[]> getControlPath()
        {
            List<int[]> pathArray = new List<int[]>();

            string[] pathCoords = linkInfo.Path.Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pathCoords.Length; i += 2)
            {
                pathArray.Add(new int[] { Math.Abs(Int32.Parse(pathCoords[i])),
                                          Math.Abs(Int32.Parse(pathCoords[i + 1]))});
            }

            return pathArray;
        }

        /// <summary>
        /// Calculate the connection point of end object
        /// </summary>
        /// <returns>Calculated coordinates</returns>
        private int[] getEndPoint()
        {
            int endX, endY;
            int prevX, prevY;
            int objectWidth = endObject.right - endObject.left;
            int objectHeight = Math.Abs(endObject.bottom) - Math.Abs(endObject.top);

            endX = endObject.left + (objectWidth / 2) + linkInfo.End.NormalizedPointX;
            endY = Math.Abs(endObject.top) + (objectHeight / 2) + linkInfo.End.NormalizedPointY;

            eObjectSide edge = eObjectSide.Default;
            prevX = this.segments[this.segments.Count - 1][0];
            prevY = this.segments[this.segments.Count - 1][1];

            if (endX == prevX) // Vertical line
            {
                edge = endY > prevY ? eObjectSide.Top : eObjectSide.Bottom;
            }
            else // Horizontal line
            {
                edge = endX > prevX ? eObjectSide.Left : eObjectSide.Right;
            }

            return calculatePosition(edge, linkInfo.End, endObject);
            
        }

        /// <summary>
        /// Calculate connection point of a given object
        /// </summary>
        /// <param name="edge">Connecting side of the object</param>
        /// <param name="point">Connecting point relative from the center of the edge</param>
        /// <param name="diagramObject">Connecting object</param>
        /// <returns>Calculated coordinates</returns>
        private int[] calculatePosition(eObjectSide edge, DiagramCoordinate point, DiagramObject diagramObject)
        {
            int startX, startY;
            int objectWidth = diagramObject.right - diagramObject.left;
            int objectHeight = Math.Abs(diagramObject.bottom) - Math.Abs(diagramObject.top);

            switch (edge)
            {
                case eObjectSide.Left:
                    startX = diagramObject.left;
                    startY = Math.Abs(diagramObject.top) + (objectHeight / 2) + point.PointY;
                    break;

                case eObjectSide.Right:
                    startX = diagramObject.right;
                    startY = Math.Abs(diagramObject.top) + (objectHeight / 2) + point.PointY;
                    break;

                case eObjectSide.Top:
                    startX = diagramObject.left + (objectWidth / 2) + point.PointX;
                    startY = Math.Abs(diagramObject.top);
                    break;

                case eObjectSide.Bottom:
                    startX = diagramObject.left + (objectWidth / 2) + point.PointX;
                    startY = Math.Abs(diagramObject.bottom);
                    break;

                default:
                    startX = 0;
                    startY = 0;
                    break;
            }

            return new int[] { startX, startY };
        }
    }
}
