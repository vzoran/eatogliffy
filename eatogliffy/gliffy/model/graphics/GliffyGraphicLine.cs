using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    public class GliffyGraphicLine : GliffyGraphic
    {
        public GliffyLine Line { get; set; }

        public GliffyGraphicLine()
        {
            Type = "Line";
        }
    }
}
