using EA;
using eatogliffy.gliffy.builder.graphics.path;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.exception;
using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections;
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
        private Repository eaRepository;
        private eLineType lineType;
        private GliffyGraphicLine gliffyLine;
        private DiagramCoordinate linkPosition;
 
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
        /// Add EA Repository for further query. Without this object the builder will fail.
        /// </summary>
        /// <param name="repository">Not null repositry object</param>
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

        public LineBuilder withLinkPosition(DiagramCoordinate linkPosition)
        {
            this.linkPosition = linkPosition;
            return this;
        }

        /// <summary>
        /// Build a gliffy line object. 
        /// </summary>
        /// <returns>Returns with the builder instance</returns>
        public LineBuilder build()
        {
            if(eaDiagramLink == null || eaConnector == null || eaRepository == null || linkPosition == null)
            {
                throw new InvalidBuilderSetupException();
            }

            this.eaDiagramLink.Update();
            LinkInfo linkInfo = new LinkInfo(eaDiagramLink);

            gliffyLine = new GliffyGraphicLine();
            gliffyLine.Line = new GliffyLine();

            gliffyLine.Line.strokeColor = BuilderTools.hexConverter(eaDiagramLink.LineColor, BuilderTools.COLOR_BLACK);
            gliffyLine.Line.startArrowRotation = "auto";
            gliffyLine.Line.endArrowRotation = "auto";
            gliffyLine.Line.interpolationType = "linear";
            gliffyLine.Line.controlPath = createControlPath(linkInfo);
            gliffyLine.Line.fillColor = "none";
            gliffyLine.Line.cornerRadius = 2;
            gliffyLine.Line.ortho = !linkInfo.IsStraight;
            gliffyLine.Line.strokeWidth = eaDiagramLink.LineWidth > 0 ? eaDiagramLink.LineWidth : 1;
            
            return this;
        }

        /// <summary>
        /// Generate list if breakpoint coordinates out of path variable of a Diagram Link.
        /// </summary>
        /// <param name="linkInfo">LinkInfo object which has parsed the DiagramLink characteristics already.</param>
        /// <returns>Generated list of coordinates</returns>
        private List<int[]> createControlPath(LinkInfo linkInfo)
        {
            PathBuilder pathBuilder;

            if(linkInfo.IsStraight)
            {
                pathBuilder = new DirectPathBuilder();
            }
            else
            {
                pathBuilder = new TreePathBuilder();
            }

            Diagram diagram = eaRepository.GetDiagramByID(eaDiagramLink.DiagramID);
            DiagramObject startObject = BuilderTools.getDiagramObjectById(diagram, eaDiagramLink.SourceInstanceUID);
            DiagramObject endObject = BuilderTools.getDiagramObjectById(diagram, eaDiagramLink.TargetInstanceUID);

            return pathBuilder
                .withStartObject(startObject)
                .withEndObject(endObject)
                .withLinkInfo(linkInfo)
                .build()
                .getPath();
        }

        /// <summary>
        /// Calculates a connection point of a DiagramObject
        /// </summary>
        /// <param name="point">Coordinates relative to the center point of the object</param>
        /// <param name="startObject">Connected Object. It is expected to be not null</param>
        /// <returns>2-length int array containing X and Y coordinate point</returns>
        private int[] getObjectPoint(DiagramCoordinate point, DiagramObject startObject)
        {
            int startX = 0, startY = 0;
            int objectWidth = startObject.right - startObject.left;
            int objectHeight = Math.Abs(startObject.bottom) - Math.Abs(startObject.top);

            startX = startObject.left + (objectWidth / 2) + point.NormalizedPointX - linkPosition.PointX;
            startY = Math.Abs(startObject.top) + (objectHeight / 2) + point.NormalizedPointY - linkPosition.PointY;

            return new int[] { startX, startY };
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
