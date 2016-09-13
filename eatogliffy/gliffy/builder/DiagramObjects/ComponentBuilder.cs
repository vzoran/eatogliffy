using EaToGliffy.Gliffy.Builder.Graphics;
using EaToGliffy.Gliffy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.DiagramObjects
{
    /// <summary>
    /// Class for converting an EA Component object
    /// </summary>
    /// <see cref="ObjectBuilder"/> 
    public class ComponentBuilder : ObjectBuilder
    {
        protected override void buildProperties(bool isParent)
        {
            base.buildProperties(isParent);
            this.gliffyObject.Uid = "com.gliffy.shape.uml.uml_v2.component.component1";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            ShapeBuilder shapeBuilder = new ShapeBuilder();

            this.gliffyObject.Graphic = shapeBuilder
                    .WithEaObject(this.eaDiagramObject)
                    .WithType(eShapeType.Component)
                    .Build()
                    .GetShape();
        }

        protected override void buildChildren()
        {
            base.buildChildren();

            GliffyParentObject gliffyParentObject = gliffyObject as GliffyParentObject;
            TextBuilder textBuilder = new TextBuilder();

            gliffyParentObject.Children = new List<GliffyObject>();
            gliffyParentObject.Children.Add(textBuilder
                .WithEaElement(this.eaElement)
                .WithEaObject(this.eaDiagramObject)
                .WithLayer(this.layerId)
                .BuildAsChild()
                .GetObject());

        }
    }
}
