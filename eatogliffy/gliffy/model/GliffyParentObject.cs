using Newtonsoft.Json;
using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyParentObject : GliffyObject
    {
        [JsonProperty(PropertyName = "children")]
        public List<GliffyObject> Children { get; set; }
    }
}
