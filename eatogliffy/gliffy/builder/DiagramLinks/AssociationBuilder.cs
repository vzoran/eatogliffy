using EaToGliffy.Gliffy.Builder.Core;
using EaToGliffy.Gliffy.Model.Graphics;
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
            this.gliffyLink.Uid = "com.gliffy.shape.uml.uml_v1.default.association";
        }
    }
}
