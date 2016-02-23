using System.Collections.Generic;

namespace eatogliffy.gliffy.model
{
    public class GliffyStage
    {
        public string background { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int maxWidth { get; set; }
        public int maxHeight { get; set; }
        public int nodeIndex { get; set; }
        public bool autoFit { get; set; }
        public bool exportBorder { get; set; }
        public bool gridOn { get; set; }
        public bool snapToGrid { get; set; }
        public bool drawingGuidesOn { get; set; }
        public bool pageBreaksOn { get; set; }
        public bool printGridOn { get; set; }
        public bool printShrinkToFit { get; set; }
        public bool printPortrait { get; set; }
        public object themeData { get; set; }
        public object printPaper { get; set; }
        public string viewportType { get; set; }

        public GliffyBox fitBB { get; set; }
        public GliffyPrintModel printModel { get; set; }
        public List<GliffyLayer> layers { get; set; }
        public List<GliffyObject> objects { get; set; }
    }
}
