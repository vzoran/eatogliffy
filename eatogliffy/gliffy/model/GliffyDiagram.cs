namespace eatogliffy.gliffy.model
{
    public class GliffyDiagram
    {
        public string contentType { get; set; }
        public string version { get; set; }
        public GliffyStage stage { get; set; }
        public GliffyMetaData metadata { get; set; }
    }
}
