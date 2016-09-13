using EA;
using EaToGliffy.Gliffy.Builder.Tools;
using EaToGliffy.Gliffy.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Graphics
{
    public class ShapeBuilder
    {
        private DiagramObject eaDiagramObject;
        private eShapeType shapeType;
        private GliffyGraphicShape gliffyGraphicShape;

        /// <summary>
        /// Setter of EA diagram object
        /// </summary>
        /// <param name="diagramObject">EA diagram object</param>
        /// <returns>Self reference</returns>
        public ShapeBuilder WithEaObject(DiagramObject diagramObject)
        {
            this.eaDiagramObject = diagramObject;
            return this;
        }

        /// <summary>
        /// Setter of selected shape type
        /// </summary>
        /// <param name="shapeType">shape type</param>
        /// <returns>Self reference</returns>
        public ShapeBuilder WithType(eShapeType shapeType)
        {
            this.shapeType = shapeType;
            return this;
        }

        /// <summary>
        /// Build a particular shape
        /// </summary>
        /// <returns>Self reference</returns>
        public ShapeBuilder Build() {
            gliffyGraphicShape = new GliffyGraphicShape();
            GliffyShape shape = new GliffyShape();

            shape.dashStyle = null;
            shape.dropShadow = false;
            shape.fillColor = BuilderTools.HexConverter(eaDiagramObject.BackgroundColor);
            shape.strokeColor = BuilderTools.HexConverter(eaDiagramObject.BorderColor, BuilderTools.COLOR_BLACK);
            shape.gradient = false;
            shape.opacity = 1;
            shape.shadowX = 0;
            shape.shadowY = 0;
            shape.tid = getTypeString();
            shape.strokeWidth = eaDiagramObject.BorderLineWidth;

            gliffyGraphicShape.Shape = shape;

            return this;
        }

        /// <summary>
        /// Returns with generated shape
        /// </summary>
        /// <returns></returns>
        public GliffyGraphicShape GetShape()
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
    }

    public enum eShapeType
    {
        Rectangle,
        Component
    }
}
