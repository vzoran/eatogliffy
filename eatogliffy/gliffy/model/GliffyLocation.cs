using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyLocation
    {
        [JsonProperty(PropertyName = "x")]
        public int XPos { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int YPos { get; set; }
    }
}
