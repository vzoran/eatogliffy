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
            this.gliffyObject.Uid = null;
            this.gliffyObject.XPos = MARGIN;
            this.gliffyObject.YPos = 0;
            this.gliffyObject.Width = this.eaDiagramObject.right - this.eaDiagramObject.left - (2 * MARGIN);
            this.gliffyObject.Height = 14;
            this.gliffyObject.Order = "auto";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            GliffyGraphicText gliffyGraphicText = new GliffyGraphicText();
            GliffyText gliffyText = new GliffyText();

            gliffyText.Html = eaElement.Name;

            gliffyGraphicText.Text = gliffyText;
            this.gliffyObject.Graphic = gliffyGraphicText;
        }
    }
}
