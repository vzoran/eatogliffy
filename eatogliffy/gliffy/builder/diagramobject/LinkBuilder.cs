using EA;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramobject
{
    public class LinkBuilder
    {
        private const int DEFAULT_WIDTH = 100;
        private const int DEFAULT_HEIGHT = 100;

        protected GliffyLink gliffyLink;
        protected DiagramLink eaDiagramLink;
        protected Connector eaConnector;
        protected string layerId = "";

        protected virtual void buildProperties()
        {
            gliffyLink.x = eaConnector.StartPointX;
            gliffyLink.y = eaConnector.StartPointY;
            gliffyLink.rotation = 0;
            gliffyLink.width = DEFAULT_WIDTH;
            gliffyLink.height = DEFAULT_HEIGHT;
            gliffyLink.order = "auto";
            gliffyLink.lockShape = false;
            gliffyLink.lockAspectRatio = false;
            gliffyLink.hidden = false;
            gliffyLink.layerId = layerId;
            gliffyLink.id = IdManager.GetId(eaConnector.ConnectorGUID);
        }

        protected virtual void buildGraphic()
        {

        }

        protected virtual void buildLinkMap()
        {

        }

        protected virtual void buildConstraints()
        {
            gliffyLink.constraints = new List<GliffyConstraint>();

            gliffyLink.startConstraint = new GliffyStartConstraint();
            gliffyLink.startConstraint.StartPositionConstraint.nodeId = IdManager.GetIdByIndex(eaConnector.SupplierID);
            gliffyLink.startConstraint.StartPositionConstraint.px = 0;
            gliffyLink.startConstraint.StartPositionConstraint.py = 0;

            gliffyLink.endConstraint = new GliffyEndConstraint();
            gliffyLink.endConstraint.EndPositionConstraint.nodeId = IdManager.GetIdByIndex(eaConnector.ClientID);
            gliffyLink.endConstraint.EndPositionConstraint.px = 0;
            gliffyLink.endConstraint.EndPositionConstraint.py = 0;
        }

        public LinkBuilder withEaConnector(Connector diagramConnector)
        {
            eaConnector = diagramConnector;
            return this;
        }

        public LinkBuilder withEaLink(DiagramLink diagramLink)
        {
            eaDiagramLink = diagramLink;
            return this;
        }

        public LinkBuilder withLayer(string layerId)
        {
            this.layerId = layerId;
            return this;
        }

        public LinkBuilder build()
        {
            gliffyLink = new GliffyLink();

            buildProperties();
            buildConstraints();
            buildGraphic();

            return this;
        }

        public GliffyObject getObject()
        {
            return gliffyLink;
        }
    }
}
