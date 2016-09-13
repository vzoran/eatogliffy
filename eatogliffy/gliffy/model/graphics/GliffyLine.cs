using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model.Graphics
{
    public class GliffyLine
    {
        [JsonProperty(PropertyName = "strokeWidth")]
        public double StrokeWidth { get; set; }

        [JsonProperty(PropertyName = "fillColor")]
        public string FillColor { get; set; }

        [JsonProperty(PropertyName = "strokeColor")]
        public string StrokeColor { get; set; }

        [JsonProperty(PropertyName = "dashStyle")]
        public string DashStyle { get; set; }

        [JsonProperty(PropertyName = "startArrow")]
        public int StartArrow { get; set; }

        [JsonProperty(PropertyName = "endArrow")]
        public int EndArrow { get; set; }

        [JsonProperty(PropertyName = "startArrowRotation")]
        public string StartArrowRotation { get; set; }

        [JsonProperty(PropertyName = "endArrowRotation")]
        public string EndArrowRotation { get; set; }

        [JsonProperty(PropertyName = "interpolationType")]
        public string InterpolationType { get; set; }

        [JsonProperty(PropertyName = "cornerRadius")]
        public int CornerRadius { get; set; }

        [JsonProperty(PropertyName = "ortho")]
        public bool Ortho { get; set; }

        [JsonProperty(PropertyName = "controlPath")]
        public List<int[]> ControlPath { get; set; }
    }
}
