using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyPrintModel
    {
        [JsonProperty(PropertyName = "pageSize")]
        public string PageSize { get; set; }

        [JsonProperty(PropertyName = "portrait")]
        public bool Portrait { get; set; }

        [JsonProperty(PropertyName = "fitToOnePage")]
        public bool FitToOnePage { get; set; }

        [JsonProperty(PropertyName = "displayPageBreaks")]
        public bool DisplayPageBreaks { get; set; }
    }
}
