using EaToGliffy.Gliffy.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.DiagramObjects
{
    /// <summary>
    /// Class for converting a Text object
    /// </summary>
    /// <see cref="ObjectBuilder"/> 
    class TextBuilder : ObjectBuilder
    {
        private readonly int MARGIN = 2;

        protected override void buildProperties(bool isParent)
        {
            base.buildProperties(isParent);
            this.gliffyObject.uid = null;
            this.gliffyObject.x = MARGIN;
            this.gliffyObject.y = 0;
            this.gliffyObject.width = this.eaDiagramObject.right - this.eaDiagramObject.left - (2 * MARGIN);
            this.gliffyObject.height = 14;
            this.gliffyObject.order = "auto";
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
    }
}
