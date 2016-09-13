using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyParentObject : GliffyObject
    {
        public List<GliffyObject> children { get; set; }
    }
}
