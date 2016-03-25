using eatogliffy.gliffy.builder.graphics;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramobject
{
    public class ComponentBuilder : ObjectBuilder
    {
        protected override void buildProperties()
        {
            base.buildProperties();
            this.gliffyObject.uid = "com.gliffy.shape.uml.uml_v2.component.component1";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            ShapeBuilder shapeBuilder = new ShapeBuilder();

            this.gliffyObject.graphic = shapeBuilder
                    .withEaObject(this.eaDiagramObject)
                    .withType(eShapeType.Component)
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
