using EA;
using EaToGliffy.Gliffy.Builder.Tools;
using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Builder.Graphics.Path
{
    /// <summary>
    /// An abstract class to support building the line characteristics based on line types.
    /// </summary>
    public abstract class PathBuilder
    {
        protected DiagramObject startObject;
        protected DiagramObject endObject;
        protected LinkInfo linkInfo;
        protected List<int[]> segments;

        /// <summary>
        /// Setter of start object.
        /// </summary>
        /// <param name="startObject">A not null diagram object.</param>
        /// <returns>self reference</returns>
        public PathBuilder WithStartObject(DiagramObject startObject)
        {
            this.startObject = startObject;
            return this;
        }

        /// <summary>
        /// Setter of the target diagram object
        /// </summary>
        /// <param name="endObject">A not null diagram object.</param>
        /// <returns>self reference</returns>
        public PathBuilder WithEndObject(DiagramObject endObject)
        {
            this.endObject = endObject;
            return this;
        }

        /// <summary>
        /// Link description
        /// </summary>
        /// <param name="linkInfo">A not null LinkInfo object.</param>
        /// <returns>self reference</returns>
        public PathBuilder WithLinkInfo(LinkInfo linkInfo)
        {
            this.linkInfo = linkInfo;
            return this;
        }

        /// <summary>
        /// Build Path segments
        /// </summary>
        /// <returns>Self reference</returns>
        public PathBuilder Build()
        {
            segments = new List<int[]> ();

            buildSegments();

            return this;
        }

        /// <summary>
        /// Getter of the result Path segments
        /// </summary>
        /// <returns>Generated path</returns>
        public List<int[]> GetPath()
        {
            return segments;
        }

        /// <summary>
        /// Create Path put of geometry
        /// </summary>
        protected virtual void buildSegments()
        {

        }
    }
}
