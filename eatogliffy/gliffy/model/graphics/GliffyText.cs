using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    class GliffyText
    {
        [JsonProperty(PropertyName = "overflow")]
        public string Overflow { get; set; }

        [JsonProperty(PropertyName = "paddingTop")]
        public int PaddingTop { get; set; }

        [JsonProperty(PropertyName = "paddingRight")]
        public int PaddingRight { get; set; }

        [JsonProperty(PropertyName = "paddingBottom")]
        public int PaddingBottom { get; set; }

        [JsonProperty(PropertyName = "paddingLeft")]
        public int PaddingLeft { get; set; }

        [JsonProperty(PropertyName = "outerPaddingTop")]
        public int OuterPaddingTop { get; set; }

        [JsonProperty(PropertyName = "outerPaddingRight")]
        public int OuterPaddingRight { get; set; }

        [JsonProperty(PropertyName = "outerPaddingBottom")]
        public int OuterPaddingBottom { get; set; }

        [JsonProperty(PropertyName = "outerPaddingLeft")]
        public int OuterPaddingLeft { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "lineTValue")]
        public object LineTValue { get; set; }

        [JsonProperty(PropertyName = "linePerpValue")]
        public object LinePerpValue { get; set; }

        [JsonProperty(PropertyName = "cardinalityType")]
        public object CardinalityType { get; set; }

        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }

        [JsonProperty(PropertyName = "tid")]
        public object Tid { get; set; }

        [JsonProperty(PropertyName = "valign")]
        public string VAlign { get; set; }

        [JsonProperty(PropertyName = "vposition")]
        public string VPosition { get; set; }

        [JsonProperty(PropertyName = "hposition")]
        public string HPosition { get; set; }
    }
}
