using EA;
using EaToGliffy.Gliffy.Model;
using EaToGliffy.Gliffy.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EaToGliffy.Gliffy.Builder.Tools;
using EaToGliffy.Gliffy.Exception;

namespace EaToGliffy.Gliffy.Builder.DiagramObjects
{
    /// <summary>
    /// Builder class for converting a generic EA object to a gliffy version
    /// </summary>
    /// <remarks>Abstract class</remarks>
    /// <see cref="ComponentBuilder"/>
    /// <see cref="RectangleBuilder"/>
    /// <see cref="TextBuilder"/>
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

        /// <summary>
        /// Setter of EA diagram object
        /// </summary>
        /// <param name="diagramObject">EA diagram object</param>
        /// <returns>Self reference</returns>
        public ObjectBuilder WithEaObject(DiagramObject diagramObject)
        {
            eaDiagramObject = diagramObject;
            return this;
        }

        /// <summary>
        /// Setter of EA diagram element
        /// </summary>
        /// <param name="diagramElement">EA diagram element</param>
        /// <returns>Self reference</returns>
        public ObjectBuilder WithEaElement(Element diagramElement)
        {
            eaElement = diagramElement;
            return this;
        }

        /// <summary>
        /// Setter of target layer
        /// </summary>
        /// <param name="layerId">ID of the target layer</param>
        /// <returns>Self reference</returns>
        public ObjectBuilder WithLayer(string layerId)
        {
            this.layerId = layerId;
            return this;
        }

        /// <summary>
        /// Build a particular diagram object as parent gliffy object
        /// </summary>
        /// <returns>Self reference</returns>
        /// <remarks>Parent means it can have other objects as children</remarks>
        /// <exception cref="InvalidBuilderSetupException">Throws if one or more related EA objects are null</exception>
        public ObjectBuilder BuildAsParent()
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

        /// <summary>
        /// Build a particular diagram object as child gliffy object
        /// </summary>
        /// <returns>Self reference</returns>
        /// <remarks>Child means it cannot have other objects as children</remarks>
        /// <exception cref="InvalidBuilderSetupException">Throws if one or more related EA objects are null</exception>
        public ObjectBuilder BuildAsChild()
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

        /// <summary>
        /// Returns with generated gliffy object
        /// </summary>
        /// <returns></returns>
        public GliffyObject GetObject()
        {
            return gliffyObject;
        }
    }
}
