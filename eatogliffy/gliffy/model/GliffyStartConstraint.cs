using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyStartConstraint
    {
        public string Type { get; private set; }
        public GliffyConstraint StartPositionConstraint { get; set; }

        public GliffyStartConstraint()
        {
            Type = "StartPositionConstraint";
            StartPositionConstraint = new GliffyConstraint();
        }
    }
}
