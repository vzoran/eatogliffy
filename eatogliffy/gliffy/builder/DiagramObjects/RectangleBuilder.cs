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
        protected override void BuildProperties(bool isParent)
        {
            base.BuildProperties(isParent);
            this.gliffyObject.Uid = "com.gliffy.shape.basic.basic_v1.default.rectangle";
        }

        protected override void BuildGraphic()
        {
            base.BuildGraphic();

            ShapeBuilder shapeBuilder = new ShapeBuilder();
            
            this.gliffyObject.Graphic = shapeBuilder
                    .WithEaObject(this.eaDiagramObject)
                    .WithType(eShapeType.Rectangle)
                    .Build()
                    .GetShape();
        }

        protected override void BuildChildren()
        {
            base.BuildChildren();

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
