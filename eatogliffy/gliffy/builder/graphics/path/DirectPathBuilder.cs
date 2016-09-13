using EA;
using EaToGliffy.Gliffy.Builder.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Graphics.Path
{
    /// <summary>
    /// Builder of direct line geometries
    /// </summary>
    /// <see cref="PathBuilder"/>
    public class DirectPathBuilder : PathBuilder
    {
        /// <summary>
        /// Create Path put of geometry.
        /// It will result a 2-item length array.
        /// </summary>
        protected override void buildSegments()
        {
            this.segments.Add(getObjectPoint(linkInfo.Start, startObject));
            this.segments.Add(getObjectPoint(linkInfo.End, endObject));

            base.buildSegments();
        }

        /// <summary>
        /// Calculates a connection point of a DiagramObject
        /// </summary>
        /// <param name="point">Coordinates relative to the center point of the object</param>
        /// <param name="startObject">Connected Object. It is expected to be not null</param>
        /// <returns>2-length int array containing X and Y coordinate point</returns>
        private int[] getObjectPoint(DiagramCoordinate point, DiagramObject startObject)
        {
            int startX, startY;
            int objectWidth = startObject.right - startObject.left;
            int objectHeight = Math.Abs(startObject.bottom) - Math.Abs(startObject.top);

            startX = startObject.left + (objectWidth / 2) + point.NormalizedPointX;
            startY = Math.Abs(startObject.top) + (objectHeight / 2) + point.NormalizedPointY;

            return new int[] { startX, startY };
        }
    }
}
