using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyMetaData
    {
        [JsonProperty(PropertyName = "analyticsProduct")]
        public string AnalyticsProduct { get; set; }

        [JsonProperty(PropertyName = "exportBorder")]
        public bool ExportBorder { get; set; }

        [JsonProperty(PropertyName = "lastSerialized")]
        public long LastSerialized { get; set; }

        [JsonProperty(PropertyName = "loadPosition")]
        public string LoadPosition { get; set; }

        [JsonProperty(PropertyName = "revision")]
        public int Revision { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "libraries")]
        public List<string> Libraries { get; set; }
    }
}
