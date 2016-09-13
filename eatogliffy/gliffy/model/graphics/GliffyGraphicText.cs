using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    class GliffyGraphicText : GliffyGraphic
    {
        public GliffyText Text { get; set; }

        public GliffyGraphicText()
        {
            Type = "Text";
        }
    }
}
