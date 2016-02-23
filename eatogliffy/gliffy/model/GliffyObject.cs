using eatogliffy.gliffy.model.graphics;

namespace eatogliffy.gliffy.model
{
    public class GliffyObject
    {
        public int x { get; set; }
        public int y { get; set; }
        public int rotation { get; set; }
        public int id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string order { get; set; }
        public bool lockAspectRatio { get; set; }
        public bool lockShape { get; set; }
        public string uid { get; set; }
        public bool hidden { get; set; }
        public string layerId { get; set; }
        public GliffyGraphic graphic { get; set; }
    }
}
