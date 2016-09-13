using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyConstraint
    {
        [JsonProperty(PropertyName = "nodeId")]
        public int NodeId { get; set; }

        [JsonProperty(PropertyName = "px")]
        public double Px { get; set; }

        [JsonProperty(PropertyName = "py")]
        public double Py { get; set; }
    }
}
