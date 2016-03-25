using EA;
using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.graphics
{
    public class ShapeBuilder
    {
        private DiagramObject eaDiagramObject;
        private eShapeType shapeType;
        private GliffyGraphicShape gliffyGraphicShape;

        public ShapeBuilder withEaObject(DiagramObject diagramObject)
        {
            this.eaDiagramObject = diagramObject;
            return this;
        }

        public ShapeBuilder withType(eShapeType shapeType)
        {
            this.shapeType = shapeType;
            return this;
        }

        public ShapeBuilder build() {
            gliffyGraphicShape = new GliffyGraphicShape();
            GliffyShape shape = new GliffyShape();

            shape.dashStyle = null;
            shape.dropShadow = false;
            shape.fillColor = hexConverter(Color.FromArgb(eaDiagramObject.BackgroundColor));
            shape.strokeColor = hexConverter(Color.FromArgb(eaDiagramObject.BorderColor));
            shape.gradient = false;
            shape.opacity = 1;
            shape.shadowX = 0;
            shape.shadowY = 0;
            shape.tid = getTypeString();
            shape.strokeWidth = eaDiagramObject.BorderLineWidth;

            gliffyGraphicShape.Shape = shape;

            return this;
        }

        public GliffyGraphicShape getShape()
        {
            return gliffyGraphicShape;
        }

        private string getTypeString()
        {
            switch (this.shapeType)
            {
                case eShapeType.Rectangle:
                    return "com.gliffy.stencil.rectangle.basic_v1";

                case eShapeType.Component:
                    return "com.gliffy.stencil.component.uml_v1";

                default:
                    return String.Empty;
            }
        }

        private string hexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }

    public enum eShapeType
    {
        Rectangle,
        Component
    }
}
