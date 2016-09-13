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

            shape.DashStyle = null;
            shape.DropShadow = false;
            shape.FillColor = BuilderTools.HexConverter(eaDiagramObject.BackgroundColor);
            shape.StrokeColor = BuilderTools.HexConverter(eaDiagramObject.BorderColor, BuilderTools.COLOR_BLACK);
            shape.Gradient = false;
            shape.Opacity = 1;
            shape.ShadowX = 0;
            shape.ShadowY = 0;
            shape.Tid = GetTypeString();
            shape.StrokeWidth = eaDiagramObject.BorderLineWidth;

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

        private string GetTypeString()
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
