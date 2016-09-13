﻿using EA;
using EaToGliffy.Gliffy.Builder.DiagramLinks;
using EaToGliffy.Gliffy.Builder.DiagramObjects;
using EaToGliffy.Gliffy.Builder.Tools;
using EaToGliffy.Gliffy.Exception;
using EaToGliffy.Gliffy.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Core
{
    /// <summary>
    /// Class for generating Gliffy's stage object
    /// </summary>
    public class StageBuilder
    {
        private GliffyStage gliffyStage;
        private Diagram eaDiagram;
        private Repository eaRepository;

        private const string ORIENTATION_PORTRAIT = "P";
        private const int GUID_LENGTH = 12;
        private const int MAX_HEIGHT = 5000;
        private const int MAX_WIDTH = 5000;
        private const string DEFAULT_BACKGROUND = "#FFFFFF";

        #region Public functions

        /// <summary>
        /// Sets the diagram should to be exported
        /// </summary>
        /// <param name="diagram">A particular EA diagram object</param>
        /// <returns>Self reference</returns>
        public StageBuilder WithEaDiagram(Diagram diagram)
        {
            eaDiagram = diagram;
            return this;
        }

        /// <summary>
        /// Sets the repository whom the diagram belongs to
        /// </summary>
        /// <param name="diagram">A particular EA repository object</param>
        /// <returns>Self reference</returns>
        public StageBuilder WithEaRepository(Repository repository)
        {
            eaRepository = repository;
            return this;
        }

        /// <summary>
        /// Builds the stage objects
        /// </summary>
        /// <returns>Self reference</returns>
        public StageBuilder Build()
        {
            if (eaDiagram == null || eaRepository == null)
            {
                throw new InvalidBuilderSetupException();
            }

            gliffyStage = new GliffyStage();
            gliffyStage.objects = new List<GliffyObject>();

            buildProperties();
            buildPrintModel();
            buildBoundaryBox();
            buildLayers();
            buildObjects();
            buildLinks();
            finalizeBuild();

            return this;
        }

        /// <summary>
        /// Returns with the generated stage object
        /// </summary>
        /// <returns>Stage object</returns>
        public GliffyStage GetStage()
        {
            return gliffyStage;
        }

        #endregion

        #region Private functions

        private void buildObjects()
        {
            IEnumerator objectEnumerator = eaDiagram.DiagramObjects.GetEnumerator();
            while(objectEnumerator.MoveNext())
            {
                DiagramObject diagramObject = (DiagramObject)objectEnumerator.Current;
                Element currentElement = eaRepository.GetElementByID(diagramObject.ElementID);
                ObjectBuilder objectBuilder = getObjectBuilder(currentElement.Type);

                if(objectBuilder != null)
                {
                    gliffyStage.objects.Add(
                    objectBuilder
                        .WithEaObject(diagramObject)
                        .WithEaElement(currentElement)
                        .WithLayer(gliffyStage.layers[0].guid)
                        .BuildAsParent()
                        .GetObject());
                }
            }
        }

        private void buildLinks()
        {
            IEnumerator linkEnumerator = eaDiagram.DiagramLinks.GetEnumerator();
            while (linkEnumerator.MoveNext())
            {
                EA.DiagramLink diagramLink = (EA.DiagramLink)linkEnumerator.Current;
                Connector currentElement = eaRepository.GetConnectorByID(diagramLink.ConnectorID);
                LinkBuilder linkBuilder = getLinkBuilder(currentElement.Type);

                if (linkBuilder != null && !diagramLink.IsHidden)
                {
                    gliffyStage.objects.Add(
                    linkBuilder
                        .WithEaRepository(eaRepository)
                        .WithEaConnector(currentElement)
                        .WithEaLink(diagramLink)
                        .WithLayer(gliffyStage.layers[0].guid)
                        .Build()
                        .GetObject());
                }
            }
        }

        private void finalizeBuild()
        {
            gliffyStage.nodeIndex = IdManager.Counter;
            gliffyStage.layers[0].nodeIndex = gliffyStage.objects.Count;
        }

        private ObjectBuilder getObjectBuilder(string eaElementType)
        {
            switch(eaElementType)
            {
                case "Boundary":
                    return new RectangleBuilder();

                case "Component":
                    return new ComponentBuilder();

                default:
                    return null;
            }
        }

        private LinkBuilder getLinkBuilder(string eaLinkType)
        {
            switch (eaLinkType)
            {
                case "Dependency":
                    return new DependecyBuilder();

                case "Association":
                default:
                    return new SimpleLineBuilder();
            }
        }

        private void buildProperties()
        {
            gliffyStage.autoFit = true;
            gliffyStage.background = DEFAULT_BACKGROUND;
            gliffyStage.drawingGuidesOn = true;
            gliffyStage.exportBorder = false;
            gliffyStage.gridOn = true;
            gliffyStage.height = eaDiagram.cy;
            gliffyStage.maxHeight = MAX_HEIGHT;
            gliffyStage.maxWidth = MAX_WIDTH;
            gliffyStage.pageBreaksOn = false;
            gliffyStage.printGridOn = false;
            gliffyStage.printPaper = null;
            gliffyStage.printPortrait = eaDiagram.Orientation.Equals(ORIENTATION_PORTRAIT);
            gliffyStage.printShrinkToFit = false;
            gliffyStage.snapToGrid = true;
            gliffyStage.themeData = null;
            gliffyStage.viewportType = "default";
            gliffyStage.width = eaDiagram.cx;
        }

        private void buildPrintModel()
        {
            GliffyPrintModel printModel = new GliffyPrintModel();
            printModel.pageSize = "a4";
            printModel.portrait = false;
            printModel.fitToOnePage = false;
            printModel.displayPageBreaks = false;

            gliffyStage.printModel = printModel;
        }

        private void buildLayers()
        {
            GliffyLayer gliffyLayer = new GliffyLayer();
            gliffyLayer.active = true;
            gliffyLayer.order = 0;
            gliffyLayer.locked = false;
            gliffyLayer.name = "Layer 0";
            gliffyLayer.visible = true;
            gliffyLayer.guid = randomString(GUID_LENGTH);

            gliffyStage.layers = new List<GliffyLayer>() { gliffyLayer };
        }

        private void buildBoundaryBox ()
        {
            GliffyBox gliffyBox = new GliffyBox();
            gliffyBox.min = new GliffyLocation();
            gliffyBox.max = new GliffyLocation();

            gliffyBox.min.x = 0;
            gliffyBox.min.y = 0;
            gliffyBox.max.x = eaDiagram.cx;
            gliffyBox.max.y = eaDiagram.cy;

            gliffyStage.fitBB = gliffyBox;
        }

        private string randomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}
