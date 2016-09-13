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

        protected virtual void BuildProperties(bool isParent)
        {
            gliffyObject.XPos = eaDiagramObject.left;
            gliffyObject.YPos = Math.Abs(eaDiagramObject.top);
            gliffyObject.Rotation = 0;
            gliffyObject.Width = eaDiagramObject.right - eaDiagramObject.left;
            gliffyObject.Height = Math.Abs(eaDiagramObject.bottom) - Math.Abs(eaDiagramObject.top);
            gliffyObject.Order = "auto";
            gliffyObject.LockShape = false;
            gliffyObject.LockAspectRatio = false;
            gliffyObject.Hidden = false;
            gliffyObject.LayerId = layerId;

            if(isParent)
            {
                gliffyObject.Id = IdManager.GetId(eaElement.ElementGUID);
            }
            else
            {
                gliffyObject.Id = IdManager.GetId();
            }
        }

        protected virtual void BuildGraphic()
        {
           
        }

        protected virtual void BuildChildren()
        {

        }

        protected virtual void BuildLinkMap()
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

            BuildProperties(true);
            BuildGraphic();
            BuildChildren();
            BuildLinkMap();

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

            BuildProperties(false);
            BuildGraphic();

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
