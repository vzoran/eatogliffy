using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyLink : GliffyObject
    {
        [JsonProperty(PropertyName = "constraints")]
        public List<GliffyConstraint> Constraints { get; set; }

        [JsonProperty(PropertyName = "startConstraint")]
        public GliffyStartConstraint StartConstraint { get; set; }

        [JsonProperty(PropertyName = "endConstraint")]
        public GliffyEndConstraint EndConstraint { get; set; }
    }
}
