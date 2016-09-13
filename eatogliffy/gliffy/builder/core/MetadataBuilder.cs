using EA;
using EaToGliffy.Gliffy.Exception;
using EaToGliffy.Gliffy.Model;
using System;
using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Builder.Core
{
    /// <summary>
    /// Class for generating Gliffy's metadata object
    /// </summary>
    public class MetadataBuilder
    {
        private GliffyMetaData gliffyData;
        private Diagram eaDiagram;

        /// <summary>
        /// Sets the diagram used as basis
        /// </summary>
        /// <param name="diagram">selected diagram</param>
        /// <returns>Self reference</returns>
        public MetadataBuilder WithEaDiagram(Diagram diagram)
        {
            eaDiagram = diagram;
            return this;
        }

        /// <summary>
        /// Builds the metadata objects
        /// </summary>
        /// <returns>Self reference</returns>
        public MetadataBuilder Build()
        {
            if(eaDiagram == null)
            {
                throw new InvalidBuilderSetupException();
            }

            gliffyData = new GliffyMetaData();

            gliffyData.LastSerialized = DateTime.Now.Ticks / 10000;
            gliffyData.AnalyticsProduct = "Confluence";
            gliffyData.ExportBorder = false;
            gliffyData.LoadPosition = "default";
            gliffyData.Revision = 0;
            gliffyData.Title = eaDiagram.Name;

            gliffyData.Libraries = new List<string>() {
                  "com.Gliffy.libraries.uml.uml_v2.class",
                  "com.Gliffy.libraries.uml.uml_v2.sequence",
                  "com.Gliffy.libraries.uml.uml_v2.activity",
                  "com.Gliffy.libraries.uml.uml_v2.state_machine",
                  "com.Gliffy.libraries.uml.uml_v2.deployment",
                  "com.Gliffy.libraries.uml.uml_v2.component",
                  "com.Gliffy.libraries.uml.uml_v2.use_case",
                  "com.Gliffy.libraries.erd.erd_v1.default",
                  "com.Gliffy.libraries.basic.basic_v1.default",
                  "com.Gliffy.libraries.images"
            };

            return this;
        }

        /// <summary>
        /// Returns with the generated metadata object
        /// </summary>
        /// <returns></returns>
        public GliffyMetaData GetMetadata()
        {
            return gliffyData;
        }
    }
}
