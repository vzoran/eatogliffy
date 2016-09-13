using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyLink : GliffyObject
    {
        public List<GliffyConstraint> constraints { get; set; }
        public GliffyStartConstraint startConstraint { get; set; }
        public GliffyEndConstraint endConstraint { get; set; }
    }
}
