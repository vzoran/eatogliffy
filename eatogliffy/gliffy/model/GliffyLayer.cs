using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyLayer
    {
        [JsonProperty(PropertyName = "guid")]
        public string Guid { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }

        [JsonProperty(PropertyName = "visible")]
        public bool Visible { get; set; }

        [JsonProperty(PropertyName = "nodeIndex")]
        public int NodeIndex { get; set; }
    }
}
