using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.model.graphics
{
    class GliffyGraphicText : GliffyGraphic
    {
        public GliffyText Text { get; set; }

        public GliffyGraphicText()
        {
            type = "Text";
        }
    }
}
