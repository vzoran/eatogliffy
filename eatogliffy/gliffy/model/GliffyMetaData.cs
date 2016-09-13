using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyMetaData
    {
        public string analyticsProduct { get; set; }
        public bool exportBorder { get; set; }
        public long lastSerialized { get; set; }
        public string loadPosition { get; set; }
        public int revision { get; set; }
        public string title { get; set; }
        public List<string> libraries { get; set; }
    }
}
