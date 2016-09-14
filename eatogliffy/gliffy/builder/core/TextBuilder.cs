using EaToGliffy.Gliffy.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Core
{
    /// <summary>
    /// Class for converting a Text object
    /// </summary>
    /// <see cref="ObjectBuilder"/> 
    class TextBuilder : ObjectBuilder
    {
        private readonly int MARGIN = 2;

        protected override void BuildProperties(bool isParent)
        {
            base.BuildProperties(isParent);
            this.gliffyObject.Uid = null;
            this.gliffyObject.XPos = MARGIN;
            this.gliffyObject.YPos = 0;
            this.gliffyObject.Width = this.eaDiagramObject.right - this.eaDiagramObject.left - (2 * MARGIN);
            this.gliffyObject.Height = 14;
            this.gliffyObject.Order = "auto";
        }

        protected override void BuildGraphic()
        {
            base.BuildGraphic();

            GliffyGraphicText gliffyGraphicText = new GliffyGraphicText();
            GliffyText gliffyText = new GliffyText();

            gliffyText.Html = eaElement.Name;

            gliffyGraphicText.Text = gliffyText;
            this.gliffyObject.Graphic = gliffyGraphicText;
        }
    }
}
