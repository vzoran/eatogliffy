using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    public class GliffyShape 
    {
        [JsonProperty(PropertyName = "tid")]
        public string Tid { get; set; }

        [JsonProperty(PropertyName = "strokeWidth")]
        public int StrokeWidth { get; set; }

        [JsonProperty(PropertyName = "strokeColor")]
        public string StrokeColor { get; set; }

        [JsonProperty(PropertyName = "fillColor")]
        public string FillColor { get; set; }

        [JsonProperty(PropertyName = "gradient")]
        public bool Gradient { get; set; }

        [JsonProperty(PropertyName = "dashStyle")]
        public object DashStyle { get; set; }

        [JsonProperty(PropertyName = "dropShadow")]
        public bool DropShadow { get; set; }

        [JsonProperty(PropertyName = "state")]
        public int State { get; set; }

        [JsonProperty(PropertyName = "opacity")]
        public int Opacity { get; set; }

        [JsonProperty(PropertyName = "shadowX")]
        public int ShadowX { get; set; }

        [JsonProperty(PropertyName = "shadowY")]
        public int ShadowY { get; set; }
    }
}
