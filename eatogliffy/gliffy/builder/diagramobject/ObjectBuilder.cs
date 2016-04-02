﻿using EA;
using eatogliffy.gliffy.model;
using eatogliffy.gliffy.builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eatogliffy.gliffy.builder.tools;

namespace eatogliffy.gliffy.builder.diagramobject
{
    public abstract class ObjectBuilder
    {
        protected GliffyObject gliffyObject;
        protected DiagramObject eaDiagramObject;
        protected Element eaElement;
        protected string layerId = "";

        protected virtual void buildProperties()
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
            gliffyObject.id = IdManager.GetNextId(eaElement.ElementGUID);
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
            gliffyObject = new GliffyParentObject();

            buildProperties();
            buildGraphic();
            buildChildren();
            buildLinkMap();

            return this;
        }

        public ObjectBuilder buildAsChild()
        {
            gliffyObject = new GliffyObject();

            buildProperties();
            buildGraphic();

            return this;
        }

        public GliffyObject getObject()
        {
            return gliffyObject;
        }
    }
}
