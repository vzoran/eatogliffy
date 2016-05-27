using EA;
using eatogliffy.gliffy.model;
using eatogliffy.gliffy.builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.exception;

namespace eatogliffy.gliffy.builder.diagramobject
{
    public abstract class ObjectBuilder
    {
        protected GliffyObject gliffyObject;
        protected DiagramObject eaDiagramObject;
        protected Element eaElement;
        protected string layerId = "";

        protected virtual void buildProperties(bool isParent)
        {
            gliffyObject.x = eaDiagramObject.left;
            gliffyObject.y = Math.Abs(eaDiagramObject.top);
            gliffyObject.rotation = 0;
            gliffyObject.width = eaDiagramObject.right - eaDiagramObject.left;
            gliffyObject.height = Math.Abs(eaDiagramObject.bottom) - Math.Abs(eaDiagramObject.top);
            gliffyObject.order = "auto";
            gliffyObject.lockShape = false;
            gliffyObject.lockAspectRatio = false;
            gliffyObject.hidden = false;
            gliffyObject.layerId = layerId;

            if(isParent)
            {
                gliffyObject.id = IdManager.GetId(eaElement.ElementGUID);
            }
            else
            {
                gliffyObject.id = IdManager.GetId();
            }
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

        public ObjectBuilder buildAsParent()
        {
            if(eaDiagramObject == null || eaElement == null || String.IsNullOrEmpty(layerId))
            {
                throw new InvalidBuilderSetupException();
            }

            gliffyObject = new GliffyParentObject();

            buildProperties(true);
            buildGraphic();
            buildChildren();
            buildLinkMap();

            return this;
        }

        public ObjectBuilder buildAsChild()
        {
            if (eaDiagramObject == null || eaElement == null || String.IsNullOrEmpty(layerId))
            {
                throw new InvalidBuilderSetupException();
            }

            gliffyObject = new GliffyObject();

            buildProperties(false);
            buildGraphic();

            return this;
        }

        public GliffyObject getObject()
        {
            return gliffyObject;
        }
    }
}
