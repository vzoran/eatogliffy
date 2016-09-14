using EaToGliffy.Gliffy.Builder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.DiagramLinks
{
    public class AssociationBuilder : LinkBuilder
    {
        protected override void BuildProperties()
        {
            base.BuildProperties();
            this.gliffyLink.Uid = "com.gliffy.shape.basic.basic_v1.default.line";
        }
    }
}
