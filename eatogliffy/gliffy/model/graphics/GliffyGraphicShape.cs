using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    public class GliffyGraphicShape : GliffyGraphic
    {
        public GliffyShape Shape { get; set; }
    
        public GliffyGraphicShape()
        {
            type = "Shape";
        }
    }
}
