﻿using EA;
using EaToGliffy.Gliffy.Builder.Graphics.Path;
using EaToGliffy.Gliffy.Builder.Tools;
using EaToGliffy.Gliffy.Exception;
using EaToGliffy.Gliffy.Model.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Graphics
{
    /// <summary>
    /// Create a Gliffy line representation out of EA Diagram Line
    /// </summary>
    public class LineBuilder
    {
        private DiagramLink eaDiagramLink;
        private Connector eaConnector;
        private Repository eaRepository;
        private GliffyGraphicLine gliffyLine;
 
        /// <summary>
        /// Add EA Connector object to the Builder. Without this object the Builder will fail.
        /// </summary>
        /// <param name="connector">Not null Connector</param>
        /// <returns>Returns with the Builder instance</returns>
        public LineBuilder WithEaConnector(Connector connector)
        {
            this.eaConnector = connector;
            return this;
        }

        /// <summary>
        /// Add EA Diagram Link object. Without this object the Builder will fail.
        /// </summary>
        /// <param name="diagramLink">Not null diagram object</param>
        /// <returns>Returns with the Builder instance</returns>
        public LineBuilder WithEaLink(EA.DiagramLink diagramLink)
        {
            this.eaDiagramLink = diagramLink;
            return this;
        }

        /// <summary>
        /// Add EA Repository for further query. Without this object the Builder will fail.
        /// </summary>
        /// <param name="repository">Not null repositry object</param>
        /// <returns>Returns with the Builder instance</returns>
        public LineBuilder WithEaRepository(Repository repository)
        {
            this.eaRepository = repository;
            return this;
        }

        /// <summary>
        /// Build a Gliffy line object. 
        /// </summary>
        /// <returns>Returns with the Builder instance</returns>
        public LineBuilder Build()
        {
            if(eaDiagramLink == null || eaConnector == null || eaRepository == null)
            {
                throw new InvalidBuilderSetupException();
            }

            this.eaDiagramLink.Update();
            LinkInfo linkInfo = new LinkInfo(eaDiagramLink);

            gliffyLine = new GliffyGraphicLine();
            gliffyLine.Line = new GliffyLine();

            gliffyLine.Line.StrokeColor = BuilderTools.HexConverter(eaDiagramLink.LineColor, BuilderTools.COLOR_BLACK);
            gliffyLine.Line.StartArrowRotation = "auto";
            gliffyLine.Line.EndArrowRotation = "auto";
            gliffyLine.Line.InterpolationType = "linear";
            gliffyLine.Line.ControlPath = CreateControlPath(linkInfo);
            gliffyLine.Line.FillColor = "none";
            gliffyLine.Line.CornerRadius = 2;
            gliffyLine.Line.Ortho = !linkInfo.IsStraight;
            gliffyLine.Line.StrokeWidth = eaDiagramLink.LineWidth > 0 ? eaDiagramLink.LineWidth : 1;
            
            return this;
        }

        /// <summary>
        /// Generate list if breakpoint coordinates out of Path variable of a Diagram Link.
        /// </summary>
        /// <param name="linkInfo">LinkInfo object which has parsed the DiagramLinks characteristics already.</param>
        /// <returns>Generated list of coordinates</returns>
        private List<int[]> CreateControlPath(LinkInfo linkInfo)
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
            DiagramObject startObject = BuilderTools.GetDiagramObjectById(diagram, eaDiagramLink.SourceInstanceUID);
            DiagramObject endObject = BuilderTools.GetDiagramObjectById(diagram, eaDiagramLink.TargetInstanceUID);

            return pathBuilder
                .WithStartObject(startObject)
                .WithEndObject(endObject)
                .WithLinkInfo(linkInfo)
                .Build()
                .GetPath();
        }

        /// <summary>
        /// Getter of the generated Gliffy line object
        /// </summary>
        /// <returns>Gliffy line object</returns>
        public GliffyGraphicLine GetLine()
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
