﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyEndConstraint
    {
        public string Type { get; private set; }
        public GliffyConstraint EndPositionConstraint { get; set; }

        public GliffyEndConstraint()
        {
            Type = "EndPositionConstraint";
            EndPositionConstraint = new GliffyConstraint();
        }
    }
}
