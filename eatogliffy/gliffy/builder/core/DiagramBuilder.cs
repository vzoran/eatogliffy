using EA;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.exception;
using eatogliffy.gliffy.model;
using System;

namespace eatogliffy.gliffy.builder.core
{
    /// <summary>
    /// Builder class for a complete diagram.
    /// </summary>
    public class DiagramBuilder
    {
        public static readonly string DEFAULT_VERSION = "1.3";
        public static readonly string DEFAULT_CONTENT_TYPE = "application/gliffy+json";
         
        private readonly GliffyDiagram gliffyDiagram;
        private Diagram eaDiagram;
        private Repository eaRepository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiagramBuilder()
        {
            gliffyDiagram = new GliffyDiagram();
        }

        /// <summary>
        /// Setter of Gliffy version used at export
        /// </summary>
        /// <param name="version">Version number</param>
        /// <returns>Self reference</returns>
        public DiagramBuilder withVersion(string version)
        {
            gliffyDiagram.version = version;
            return this;
        }

        /// <summary>
        /// Setter of content type.
        /// </summary>
        /// <param name="contentType">Target content type</param>
        /// <returns>Self reference</returns>
        public DiagramBuilder withContentType(string contentType)
        {
            gliffyDiagram.contentType = contentType;
            return this;
        }

        /// <summary>
        /// Select the active diagram of a given repository
        /// </summary>
        /// <param name="repository">An opened EA repository</param>
        /// <returns>Self reference</returns>
        public DiagramBuilder fromActiveDiagram (Repository repository)
        {
            if(repository == null)
            {
                throw new NullReferenceException("Repository is empty or invalid.");
            }
            eaRepository = repository;
            eaDiagram = repository.GetCurrentDiagram();
            return this;
        }

        /// <summary>
        /// Build a glffy diagram out of an EA diagram object.
        /// </summary>
        /// <returns>Self reference</returns>
        public DiagramBuilder build()
        {
            if(eaDiagram == null || eaRepository == null)
            {
                throw new InvalidBuilderSetupException();
            }

            IdManager.Initialize(eaRepository);
            IdManager.Reset();

            buildStage();
            buildMetadata();

            return this;
        }

        private void buildStage()
        {
            StageBuilder stageBuilder = new StageBuilder();
            gliffyDiagram.stage = stageBuilder
                .withEaRepository(eaRepository)
                .withEaDiagram(eaDiagram)
                .build()
                .getStage();
        }

        private void buildMetadata()
        {
            MetadataBuilder metadataBuilder = new MetadataBuilder();
            gliffyDiagram.metadata = metadataBuilder
                .withEaDiagram(eaDiagram)
                .build()
                .getMetadata();
        }

        /// <summary>
        /// Retrieve the generated glffy diagram objects.
        /// </summary>
        /// <returns>Generated diagram</returns>
        public GliffyDiagram getDiagram()
        {
            return gliffyDiagram;
        }
    }
}
