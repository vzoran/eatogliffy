using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.model.graphics
{
    public class GliffyLine
    {
        public double strokeWidth { get; set; }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string dashStyle { get; set; }
        public int startArrow { get; set; }
        public int endArrow { get; set; }
        public string startArrowRotation { get; set; }
        public string endArrowRotation { get; set; }
        public string interpolationType { get; set; }
        public int cornerRadius { get; set; }
        public bool ortho { get; set; }
        public List<int[]> controlPath { get; set; }
    }
}
