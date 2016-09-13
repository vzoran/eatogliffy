namespace EaToGliffy.Gliffy.Model
{
    public class GliffyLayer
    {
        public string guid { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public bool locked { get; set; }
        public bool visible { get; set; }
        public int nodeIndex { get; set; }
    }
}
