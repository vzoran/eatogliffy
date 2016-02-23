using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramobject
{
    class TextBuilder : ObjectBuilder
    {
        protected override void buildProperties()
        {
            base.buildProperties();
            this.gliffyObject.uid = null;
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            GliffyGraphicText gliffyGraphicText = new GliffyGraphicText();
            GliffyText gliffyText = new GliffyText();

            gliffyText.html = eaElement.Name;

            gliffyGraphicText.Text = gliffyText;
            this.gliffyObject.graphic = gliffyGraphicText;
        }

        protected override void buildChildren()
        {
            base.buildChildren();
        }
    }
}
