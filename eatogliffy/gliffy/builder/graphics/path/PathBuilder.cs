using EA;
using eatogliffy.gliffy.builder.tools;
using System.Collections.Generic;

namespace eatogliffy.gliffy.builder.graphics.path
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
        public PathBuilder withStartObject(DiagramObject startObject)
        {
            this.startObject = startObject;
            return this;
        }

        /// <summary>
        /// Setter of the target diagram object
        /// </summary>
        /// <param name="endObject">A not null diagram object.</param>
        /// <returns>self reference</returns>
        public PathBuilder withEndObject(DiagramObject endObject)
        {
            this.endObject = endObject;
            return this;
        }

        /// <summary>
        /// Link description
        /// </summary>
        /// <param name="linkInfo">A not null LinkInfo object.</param>
        /// <returns>self reference</returns>
        public PathBuilder withLinkInfo(LinkInfo linkInfo)
        {
            this.linkInfo = linkInfo;
            return this;
        }

        /// <summary>
        /// Create path put of geometry
        /// </summary>
        protected virtual void buildSegments()
        {

        }

        /// <summary>
        /// Build path segments
        /// </summary>
        /// <returns></returns>
        public PathBuilder build()
        {
            segments = new List<int[]> ();

            buildSegments();

            return this;
        }

        /// <summary>
        /// Getter of the result path segments
        /// </summary>
        /// <returns></returns>
        public List<int[]> getPath()
        {
            return segments;
        }
    }
}
