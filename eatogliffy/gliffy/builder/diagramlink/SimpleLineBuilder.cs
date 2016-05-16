﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramlink
{
    public class SimpleLineBuilder : LinkBuilder
    {
        protected override void buildProperties()
        {
            base.buildProperties();
            this.gliffyLink.uid = "com.gliffy.shape.basic.basic_v1.default.line";
        }
    }
}
