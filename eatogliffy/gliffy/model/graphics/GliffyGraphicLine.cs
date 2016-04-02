using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.model.graphics
{
    public class GliffyGraphicLine : GliffyGraphic
    {
        public GliffyLine Line { get; set; }

        public GliffyGraphicLine()
        {
            type = "Line";
        }
    }
}
