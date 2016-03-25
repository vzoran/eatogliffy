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
        private const string COLOR_DEFAULT = "#FFFFFF";
        private const string COLOR_BLACK = "#000000";

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
            shape.fillColor = hexConverter(eaDiagramObject.BackgroundColor);
            shape.strokeColor = hexConverter(eaDiagramObject.BorderColor, COLOR_BLACK);
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

        private string hexConverter(int color)
        {
            return hexConverter(color, null);
        }

        private string hexConverter(int color, string overrideDefault)
        {
            var b = ((color >> 16) & 0xff);
            var g = ((color >> 8) & 0xff);
            var r = (color & 0xff);

            string colorStr = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");

            if(colorStr.Equals(COLOR_DEFAULT) && !String.IsNullOrEmpty( overrideDefault))
            {
                colorStr = overrideDefault;
            }

            return colorStr;
        }
    }

    public enum eShapeType
    {
        Rectangle,
        Component
    }
}
