using System.Collections.Generic;

namespace eatogliffy.gliffy.model
{
    public class GliffyParentObject : GliffySimpleObject
    {
        public List<GliffySimpleObject> children { get; set; }
    }
}
