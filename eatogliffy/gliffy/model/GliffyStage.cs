using Newtonsoft.Json;
using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyStage
    {
        [JsonProperty(PropertyName = "background")]
        public string Background { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "maxWidth")]
        public int MaxWidth { get; set; }

        [JsonProperty(PropertyName = "maxHeight")]
        public int MaxHeight { get; set; }

        [JsonProperty(PropertyName = "nodeIndex")]
        public int NodeIndex { get; set; }

        [JsonProperty(PropertyName = "autoFit")]
        public bool AutoFit { get; set; }

        [JsonProperty(PropertyName = "exportBorder")]
        public bool ExportBorder { get; set; }

        [JsonProperty(PropertyName = "gridOn")]
        public bool GridOn { get; set; }

        [JsonProperty(PropertyName = "snapToGrid")]
        public bool SnapToGrid { get; set; }

        [JsonProperty(PropertyName = "drawingGuidesOn")]
        public bool DrawingGuidesOn { get; set; }

        [JsonProperty(PropertyName = "pageBreaksOn")]
        public bool PageBreaksOn { get; set; }

        [JsonProperty(PropertyName = "printGridOn")]
        public bool PrintGridOn { get; set; }

        [JsonProperty(PropertyName = "printShrinkToFit")]
        public bool PrintShrinkToFit { get; set; }

        [JsonProperty(PropertyName = "printPortrait")]
        public bool PrintPortrait { get; set; }

        [JsonProperty(PropertyName = "themeData")]
        public object ThemeData { get; set; }

        [JsonProperty(PropertyName = "printPaper")]
        public object PrintPaper { get; set; }

        [JsonProperty(PropertyName = "viewportType")]
        public string ViewportType { get; set; }

        [JsonProperty(PropertyName = "fitBB")]
        public GliffyBox FitBB { get; set; }

        [JsonProperty(PropertyName = "printModel")]
        public GliffyPrintModel PrintModel { get; set; }

        [JsonProperty(PropertyName = "layers")]
        public List<GliffyLayer> Layers { get; set; }

        [JsonProperty(PropertyName = "objects")]
        public List<GliffyObject> Objects { get; set; }
    }
}
