using EA;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.exception;
using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.graphics
{
    /// <summary>
    /// Create a gliffy line representation out of EA Diagram Line
    /// </summary>
    public class LineBuilder
    {
        private DiagramLink eaDiagramLink;
        private Connector eaConnector;
        private eLineType lineType;
        private GliffyGraphicLine gliffyLine;
        private Repository eaRepository;

        /// <summary>
        /// Add EA Connector object to the builder. Without this object the builder will fail.
        /// </summary>
        /// <param name="connector">Not null Connector</param>
        /// <returns>Returns with the builder instance</returns>
        public LineBuilder withEaConnector(Connector connector)
        {
            this.eaConnector = connector;
            return this;
        }

        /// <summary>
        /// Add EA Diagram Link object. Without this object the builder will fail.
        /// </summary>
        /// <param name="diagramLink">Not null diagram object</param>
        /// <returns>Returns with the builder instance</returns>
        public LineBuilder withEaLink(DiagramLink diagramLink)
        {
            this.eaDiagramLink = diagramLink;
            return this;
        }

        /// <summary>
        /// Add EA Diagram Repository object. Without this object the builder will fail.
        /// </summary>
        /// <param name="repository"></param>
        /// <returns>Returns with the builder instance</returns>
        public LineBuilder withEaRepository(Repository repository)
        {
            this.eaRepository = repository;
            return this;
        }

        /// <summary>
        /// Set line type
        /// </summary>
        /// <param name="lineType">Line type</param>
        /// <see cref="eLineType"/>
        /// <returns>Returns with the builder instance</returns>
        public LineBuilder withType(eLineType lineType)
        {
            this.lineType = lineType;
            return this;
        }

        /// <summary>
        /// Build a gliffy line object. 
        /// </summary>
        /// <returns>Returns with the builder instance</returns>
        public LineBuilder build()
        {
            if(eaDiagramLink == null || eaConnector == null || eaRepository == null)
            {
                throw new InvalidBuilderSetupException();
            }

            gliffyLine = new GliffyGraphicLine();
            gliffyLine.Line = new GliffyLine();

            gliffyLine.Line.strokeColor = BuilderTools.hexConverter(eaDiagramLink.LineColor, BuilderTools.COLOR_BLACK);
            gliffyLine.Line.startArrowRotation = "auto";
            gliffyLine.Line.endArrowRotation = "auto";
            gliffyLine.Line.interpolationType = "linear";
            gliffyLine.Line.controlPath = getControlPath(eaDiagramLink.Path);
            gliffyLine.Line.fillColor = "none";
            gliffyLine.Line.cornerRadius = 2;
            gliffyLine.Line.ortho = true;
            gliffyLine.Line.strokeWidth = eaDiagramLink.LineWidth > 0 ? eaDiagramLink.LineWidth : 1;

            LinkGeometry linkGeometry = new LinkGeometry(eaDiagramLink.Geometry);

            return this;
        }

        /// <summary>
        /// Generate list if breakpoint coordinates out of path variable of a Diagram Link.
        /// </summary>
        /// <param name="pathString">Path of the Diagram link.</param>
        /// <returns>Generated list of coordinates</returns>
        private List<int[]> getControlPath(string pathString)
        {
            List<int[]> retList = new List<int[]>();
            string[] pathCoords = pathString.Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        
            for(int i = 0; i < pathCoords.Length; i += 2)
            {
                retList.Add(new int[] { Math.Abs(Int32.Parse(pathCoords[i])), Math.Abs(Int32.Parse(pathCoords[i + 1])) });
            }
            
            return retList;
        }

        /// <summary>
        /// Getter of the generated gliffy line object
        /// </summary>
        /// <returns>Gliffy line object</returns>
        public GliffyGraphicLine getLine()
        {
            return gliffyLine;
        }
    }

    /// <summary>
    /// Line type desrciptors
    /// </summary>
    public enum eLineType
    {
        Dependency
    }
}
