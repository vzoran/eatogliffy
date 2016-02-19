using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.model
{
    public class GliffyDiagram
    {
        public string contentType { get; set; }
        public string version { get; set; }
        public GliffyStage stage { get; set; }
    }
}
