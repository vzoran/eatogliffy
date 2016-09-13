using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    public class GliffyShape 
    {
        public string tid { get; set; }
        public int strokeWidth { get; set; }
        public string strokeColor { get; set; }
        public string fillColor { get; set; }
        public bool gradient { get; set; }
        public object dashStyle { get; set; }
        public bool dropShadow { get; set; }
        public int state { get; set; }
        public int opacity { get; set; }
        public int shadowX { get; set; }
        public int shadowY { get; set; }
    }
}
