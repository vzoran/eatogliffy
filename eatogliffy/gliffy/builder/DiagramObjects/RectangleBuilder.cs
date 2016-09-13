using EaToGliffy.Gliffy.Builder.Graphics;
using EaToGliffy.Gliffy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.DiagramObjects
{
    /// <summary>
    /// Class for converting an EA Rectangle object
    /// </summary>
    /// <see cref="ObjectBuilder"/> 
    public class RectangleBuilder : ObjectBuilder
    {
        protected override void buildProperties(bool isParent)
        {
            base.buildProperties(isParent);
            this.gliffyObject.uid = "com.gliffy.shape.basic.basic_v1.default.rectangle";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            ShapeBuilder shapeBuilder = new ShapeBuilder();
            
            this.gliffyObject.graphic = shapeBuilder
                    .WithEaObject(this.eaDiagramObject)
                    .WithType(eShapeType.Rectangle)
                    .Build()
                    .GetShape();
        }

        protected override void buildChildren()
        {
            base.buildChildren();

            GliffyParentObject gliffyParentObject = gliffyObject as GliffyParentObject;
            TextBuilder textBuilder = new TextBuilder();

            gliffyParentObject.children = new List<GliffyObject>();
            gliffyParentObject.children.Add(textBuilder
                .WithEaElement(this.eaElement)
                .WithEaObject(this.eaDiagramObject)
                .WithLayer(this.layerId)
                .BuildAsChild()
                .GetObject());

        }
    }
}
