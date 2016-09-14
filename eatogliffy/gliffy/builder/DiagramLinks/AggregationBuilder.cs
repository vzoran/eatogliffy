using EaToGliffy.Gliffy.Builder.Core;
using EaToGliffy.Gliffy.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.DiagramLinks
{
    public class AggregationBuilder : LinkBuilder
    {
        protected override void BuildProperties()
        {
            base.BuildProperties();
            this.gliffyLink.Uid = "com.gliffy.shape.uml.uml_v1.default.aggregation";
        }

        protected override void BuildGraphic()
        {
            base.BuildGraphic();
            GliffyGraphicLine line = this.gliffyLink.Graphic as GliffyGraphicLine;

            if (line != null && line.Line != null)
            {
                line.Line.EndArrow = 5;
                line.Line.StartArrow = 0;
            }
        }
    }
}
