using EA;
using EaToGliffy.Gliffy.Builder.Graphics;
using EaToGliffy.Gliffy.Builder.Tools;
using EaToGliffy.Gliffy.Exception;
using EaToGliffy.Gliffy.Model;
using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Builder.DiagramLinks
{
    public abstract class LinkBuilder
    {
        private const int DEFAULT_WIDTH = 100;
        private const int DEFAULT_HEIGHT = 100;

        protected GliffyLink gliffyLink;
        protected DiagramLink eaDiagramLink;
        protected Connector eaConnector;
        protected Repository eaRepository;
        protected string layerId = "";

        protected virtual void buildProperties()
        {
            gliffyLink.XPos = 0;
            gliffyLink.YPos = 0;
            gliffyLink.Rotation = 0;
            gliffyLink.Width = DEFAULT_WIDTH;
            gliffyLink.Height = DEFAULT_HEIGHT;
            gliffyLink.Order = "auto";
            gliffyLink.LockShape = false;
            gliffyLink.LockAspectRatio = false;
            gliffyLink.Hidden = false;
            gliffyLink.LayerId = layerId;
            gliffyLink.Id = IdManager.GetId(eaConnector.ConnectorGUID);
        }

        protected virtual void buildGraphic()
        {
            LineBuilder lineBuilder = new LineBuilder();
            this.gliffyLink.Graphic = lineBuilder
                .WithEaRepository(eaRepository)
                .WithEaConnector(eaConnector)
                .WithEaLink(this.eaDiagramLink)
                .WithType(eLineType.Dependency)
                .Build()
                .GetLine();
        }

        protected virtual void buildLinkMap()
        {

        }

        protected virtual void buildConstraints()
        {
            gliffyLink.Constraints = new List<GliffyConstraint>();

            gliffyLink.StartConstraint = new GliffyStartConstraint();
            gliffyLink.StartConstraint.StartPositionConstraint.NodeId = IdManager.GetIdByIndex(eaConnector.SupplierID);
            gliffyLink.StartConstraint.StartPositionConstraint.Px = 0;
            gliffyLink.StartConstraint.StartPositionConstraint.Py = 0.5;

            gliffyLink.EndConstraint = new GliffyEndConstraint();
            gliffyLink.EndConstraint.EndPositionConstraint.NodeId = IdManager.GetIdByIndex(eaConnector.ClientID);
            gliffyLink.EndConstraint.EndPositionConstraint.Px = 0;
            gliffyLink.EndConstraint.EndPositionConstraint.Py = 0.5;
        }

        /// <summary>
        /// Setter of the EA connector object
        /// </summary>
        /// <param name="diagramConnector">Connector object</param>
        /// <returns>Self reference</returns>
        public LinkBuilder WithEaConnector(Connector diagramConnector)
        {
            eaConnector = diagramConnector;
            return this;
        }

        /// <summary>
        /// Setter of the EA diagram link object
        /// </summary>
        /// <param name="diagramLink">EA diagram link object</param>
        /// <returns>Self reference</returns>
        public LinkBuilder WithEaLink(DiagramLink diagramLink)
        {
            eaDiagramLink = diagramLink;
            return this;
        }

        /// <summary>
        /// Setter of EA repository
        /// </summary>
        /// <param name="repository">EA repository</param>
        /// <returns>Self reference</returns>
        public LinkBuilder WithEaRepository(Repository repository)
        {
            eaRepository = repository;
            return this;
        }

        /// <summary>
        /// Setter of a given layer
        /// </summary>
        /// <param name="layerId">Id of the selected layer</param>
        /// <returns>Self reference</returns>
        public LinkBuilder WithLayer(string layerId)
        {
            this.layerId = layerId;
            return this;
        }

        /// <summary>
        /// Build a gliffy link
        /// </summary>
        /// <returns>Self reference</returns>
        public LinkBuilder Build()
        {
            if(eaDiagramLink == null || eaConnector == null || eaRepository == null || string.IsNullOrEmpty(layerId))
            {
                throw new InvalidBuilderSetupException();
            }

            gliffyLink = new GliffyLink();

            buildProperties();
            buildConstraints();
            buildGraphic();

            return this;
        }

        /// <summary>
        /// Returns with generated object
        /// </summary>
        /// <returns>Generated Gliffy link</returns>
        public GliffyObject GetObject()
        {
            return gliffyLink;
        }
    }
}
