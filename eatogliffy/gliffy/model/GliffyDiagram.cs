using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyDiagram
    {
        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "stage")]
        public GliffyStage Stage { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public GliffyMetaData Metadata { get; set; }
    }
}
