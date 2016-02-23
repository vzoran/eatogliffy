using System.Collections.Generic;

namespace eatogliffy.gliffy.model
{
    public class GliffyParentObject : GliffyObject
    {
        public List<GliffyObject> children { get; set; }
    }
}
