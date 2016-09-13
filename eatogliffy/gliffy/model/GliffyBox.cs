using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyBox
    {
        [JsonProperty(PropertyName = "min")]
        public GliffyLocation Min { get; set; }

        [JsonProperty(PropertyName = "max")]
        public GliffyLocation Max { get; set; }
    }
}
