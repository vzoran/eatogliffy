using EA;
using eatogliffy.gliffy.exception;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;

namespace eatogliffy.gliffy.builder.core
{
    /// <summary>
    /// Class for generating Gliffy's metadata object
    /// </summary>
    class MetadataBuilder
    {
        private GliffyMetaData gliffyData;
        private Diagram eaDiagram;

        /// <summary>
        /// Sets the diagram used as basis
        /// </summary>
        /// <param name="diagram">selected diagram</param>
        /// <returns>Self reference</returns>
        public MetadataBuilder withEaDiagram(Diagram diagram)
        {
            eaDiagram = diagram;
            return this;
        }

        /// <summary>
        /// Builds the metadata objects
        /// </summary>
        /// <returns>Self reference</returns>
        public MetadataBuilder build()
        {
            if(eaDiagram == null)
            {
                throw new InvalidBuilderSetupException();
            }

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

        /// <summary>
        /// Returns with the generated metadata object
        /// </summary>
        /// <returns></returns>
        public GliffyMetaData getMetadata()
        {
            return gliffyData;
        }
    }
}
