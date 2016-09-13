using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    class GliffyText
    {
        public string overflow { get; set; }
        public int paddingTop { get; set; }
        public int paddingRight { get; set; }
        public int paddingBottom { get; set; }
        public int paddingLeft { get; set; }
        public int outerPaddingTop { get; set; }
        public int outerPaddingRight { get; set; }
        public int outerPaddingBottom { get; set; }
        public int outerPaddingLeft { get; set; }
        public string type { get; set; }
        public object lineTValue { get; set; }
        public object linePerpValue { get; set; }
        public object cardinalityType { get; set; }
        public string html { get; set; }
        public object tid { get; set; }
        public string valign { get; set; }
        public string vposition { get; set; }
        public string hposition { get; set; }
    }
}
