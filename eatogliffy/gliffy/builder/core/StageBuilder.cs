using EA;
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
            gliffyStage.Objects = new List<GliffyObject>();

            BuildProperties();
            BuildPrintModel();
            BuildBoundaryBox();
            BuildLayers();
            BuildObjects();
            BuildLinks();
            FinalizeBuild();

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

        private void BuildObjects()
        {
            IEnumerator objectEnumerator = eaDiagram.DiagramObjects.GetEnumerator();
            while(objectEnumerator.MoveNext())
            {
                DiagramObject diagramObject = (DiagramObject)objectEnumerator.Current;
                Element currentElement = eaRepository.GetElementByID(diagramObject.ElementID);
                ObjectBuilder objectBuilder = GetObjectBuilder(currentElement.Type);

                if(objectBuilder != null)
                {
                    gliffyStage.Objects.Add(
                    objectBuilder
                        .WithEaObject(diagramObject)
                        .WithEaElement(currentElement)
                        .WithLayer(gliffyStage.Layers[0].Guid)
                        .BuildAsParent()
                        .GetObject());
                }
            }
        }

        private void BuildLinks()
        {
            IEnumerator linkEnumerator = eaDiagram.DiagramLinks.GetEnumerator();
            while (linkEnumerator.MoveNext())
            {
                EA.DiagramLink diagramLink = (EA.DiagramLink)linkEnumerator.Current;
                Connector currentElement = eaRepository.GetConnectorByID(diagramLink.ConnectorID);
                LinkBuilder linkBuilder = GetLinkBuilder(currentElement.Type);

                if (linkBuilder != null && !diagramLink.IsHidden)
                {
                    gliffyStage.Objects.Add(
                    linkBuilder
                        .WithEaRepository(eaRepository)
                        .WithEaConnector(currentElement)
                        .WithEaLink(diagramLink)
                        .WithLayer(gliffyStage.Layers[0].Guid)
                        .Build()
                        .GetObject());
                }
            }
        }

        private void FinalizeBuild()
        {
            gliffyStage.NodeIndex = IdManager.Counter;
            gliffyStage.Layers[0].NodeIndex = gliffyStage.Objects.Count;
        }

        private ObjectBuilder GetObjectBuilder(string eaElementType)
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

        private LinkBuilder GetLinkBuilder(string eaLinkType)
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

        private void BuildProperties()
        {
            gliffyStage.AutoFit = true;
            gliffyStage.Background = DEFAULT_BACKGROUND;
            gliffyStage.DrawingGuidesOn = true;
            gliffyStage.ExportBorder = false;
            gliffyStage.GridOn = true;
            gliffyStage.Height = eaDiagram.cy;
            gliffyStage.MaxHeight = MAX_HEIGHT;
            gliffyStage.MaxWidth = MAX_WIDTH;
            gliffyStage.PageBreaksOn = false;
            gliffyStage.PrintGridOn = false;
            gliffyStage.PrintPaper = null;
            gliffyStage.PrintPortrait = eaDiagram.Orientation.Equals(ORIENTATION_PORTRAIT);
            gliffyStage.PrintShrinkToFit = false;
            gliffyStage.SnapToGrid = true;
            gliffyStage.ThemeData = null;
            gliffyStage.ViewportType = "default";
            gliffyStage.Width = eaDiagram.cx;
        }

        private void BuildPrintModel()
        {
            GliffyPrintModel printModel = new GliffyPrintModel();
            printModel.PageSize = "a4";
            printModel.Portrait = false;
            printModel.FitToOnePage = false;
            printModel.DisplayPageBreaks = false;

            gliffyStage.PrintModel = printModel;
        }

        private void BuildLayers()
        {
            GliffyLayer gliffyLayer = new GliffyLayer();
            gliffyLayer.Active = true;
            gliffyLayer.Order = 0;
            gliffyLayer.Locked = false;
            gliffyLayer.Name = "Layer 0";
            gliffyLayer.Visible = true;
            gliffyLayer.Guid = RandomString(GUID_LENGTH);

            gliffyStage.Layers = new List<GliffyLayer>() { gliffyLayer };
        }

        private void BuildBoundaryBox ()
        {
            GliffyBox gliffyBox = new GliffyBox();
            gliffyBox.Min = new GliffyLocation();
            gliffyBox.Max = new GliffyLocation();

            gliffyBox.Min.XPos = 0;
            gliffyBox.Min.YPos = 0;
            gliffyBox.Max.XPos = eaDiagram.cx;
            gliffyBox.Max.YPos = eaDiagram.cy;

            gliffyStage.FitBB = gliffyBox;
        }

        private string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}
