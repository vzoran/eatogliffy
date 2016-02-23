using EA;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramobject
{
    public abstract class ObjectBuilder
    {
        protected GliffyParentObject gliffyObject;
        protected DiagramObject eaDiagramObject;
        protected Element eaElement;
        protected string layerId = "";

        protected virtual void buildProperties()
        {
            gliffyObject.x = eaDiagramObject.left;
            gliffyObject.y = eaDiagramObject.top;
            gliffyObject.rotation = 0;
            gliffyObject.width = eaDiagramObject.right - eaDiagramObject.left;
            gliffyObject.height = eaDiagramObject.bottom - eaDiagramObject.top;
            gliffyObject.order = "auto";
            gliffyObject.lockShape = false;
            gliffyObject.lockAspectRatio = false;
            gliffyObject.hidden = false;
            gliffyObject.layerId = layerId;
            gliffyObject.id = eaDiagramObject.ElementID;
        }

        protected virtual void buildGraphic()
        {
           
        }

        protected virtual void buildChildren()
        {

        }

        protected virtual void buildLinkMap()
        {

        }

        public ObjectBuilder withEaObject(DiagramObject diagramObject)
        {
            eaDiagramObject = diagramObject;
            return this;
        }

        public ObjectBuilder withEaElement(Element diagramElement)
        {
            eaElement = diagramElement;
            return this;
        }

        public ObjectBuilder withLayer(string layerId)
        {
            this.layerId = layerId;
            return this;
        }

        public ObjectBuilder build()
        {
            gliffyObject = new GliffyParentObject();

            buildProperties();
            buildGraphic();
            buildChildren();
            buildLinkMap();

            return this;
        }

        public GliffyParentObject getObject()
        {
            return gliffyObject;
        }
    }
}
