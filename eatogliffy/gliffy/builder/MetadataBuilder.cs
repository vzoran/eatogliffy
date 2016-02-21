using EA;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder
{
    class MetadataBuilder
    {
        private GliffyMetaData gliffyData;
        private Diagram eaDiagram;

        public MetadataBuilder()
        {
            
        }

        public MetadataBuilder withEaDiagram(Diagram diagram)
        {
            eaDiagram = diagram;
            return this;
        }

        public MetadataBuilder build()
        {
            gliffyData = new GliffyMetaData();

            gliffyData.lastSerialized = DateTime.Now.Ticks / 10000;
            gliffyData.analyticsProduct = "Confluence";
            gliffyData.exportBorder = false;
            gliffyData.loadPosition = "default";
            gliffyData.revision = 0;
            gliffyData.title = eaDiagram.Name;

            gliffyData.libraries = new List<string>() {
                  "com.gliffy.libraries.uml.uml_v2.class",
                  "com.gliffy.libraries.uml.uml_v2.sequence",
                  "com.gliffy.libraries.uml.uml_v2.activity",
                  "com.gliffy.libraries.uml.uml_v2.state_machine",
                  "com.gliffy.libraries.uml.uml_v2.deployment",
                  "com.gliffy.libraries.uml.uml_v2.component",
                  "com.gliffy.libraries.uml.uml_v2.use_case",
                  "com.gliffy.libraries.erd.erd_v1.default",
                  "com.gliffy.libraries.basic.basic_v1.default",
                  "com.gliffy.libraries.images"
            };

            return this;
        }

        public GliffyMetaData getMetadata()
        {
            return gliffyData;
        }
    }
}
