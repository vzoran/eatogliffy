using eatogliffy.gliffy.builder.graphics;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramobject
{
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
                    .withEaObject(this.eaDiagramObject)
                    .withType(eShapeType.Rectangle)
                    .build()
                    .getShape();
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
    }
}
