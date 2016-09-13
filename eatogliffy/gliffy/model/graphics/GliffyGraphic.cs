using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    public abstract class GliffyGraphic
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
