using eatogliffy.gliffy.model;
using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramobject
{
    public class RectangleBuilder : ObjectBuilder
    {
        protected override void buildProperties()
        {
            base.buildProperties();
            this.gliffyObject.uid = "com.gliffy.shape.basic.basic_v1.default.rectangle";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            GliffyGraphicShape gliffyGraphicShape = new GliffyGraphicShape();
            GliffyShape shape = new GliffyShape();
            shape.dashStyle = null;
            shape.dropShadow = false;
            shape.fillColor = hexConverter(Color.FromArgb(eaDiagramObject.BackgroundColor));
            shape.strokeColor = hexConverter(Color.FromArgb(eaDiagramObject.BorderColor));
            shape.gradient = false;
            shape.opacity = 1;
            shape.shadowX = 0;
            shape.shadowY = 0;
            shape.tid = "com.gliffy.stencil.rectangle.basic_v1";
            shape.strokeWidth = eaDiagramObject.BorderLineWidth;

            gliffyGraphicShape.Shape = shape;
            this.gliffyObject.graphic = gliffyGraphicShape;
        }

        protected override void buildChildren()
        {
            base.buildChildren();

            GliffyParentObject gliffyParentObject = gliffyObject as GliffyParentObject;
            TextBuilder textBuilder = new TextBuilder();

            gliffyParentObject.children = new List<GliffyObject>();
            gliffyParentObject.children.Add(textBuilder
                .withEaElement(this.eaElement)
                .withEaObject(this.eaDiagramObject)
                .withLayer(this.layerId)
                .buildAsChild()
                .getObject());

        }

        private string hexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
