using EA;
using EaToGliffy.Gliffy.Builder.Tools;
using EaToGliffy.Gliffy.Exception;
using EaToGliffy.Gliffy.Model;
using System;

namespace EaToGliffy.Gliffy.Builder.Core
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

        #region Public functions

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
        public DiagramBuilder WithVersion(string version)
        {
            gliffyDiagram.Version = version;
            return this;
        }

        /// <summary>
        /// Setter of content type.
        /// </summary>
        /// <param name="contentType">Target content type</param>
        /// <returns>Self reference</returns>
        public DiagramBuilder WithContentType(string contentType)
        {
            gliffyDiagram.ContentType = contentType;
            return this;
        }

        /// <summary>
        /// Select the active diagram of a given repository
        /// </summary>
        /// <param name="repository">An opened EA repository</param>
        /// <returns>Self reference</returns>
        public DiagramBuilder FromActiveDiagram (Repository repository)
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
        public DiagramBuilder Build()
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

        /// <summary>
        /// Retrieve the generated glffy diagram objects.
        /// </summary>
        /// <returns>Generated diagram</returns>
        public GliffyDiagram GetDiagram()
        {
            return gliffyDiagram;
        }

        #endregion

        #region Private functions
        private void buildStage()
        {
            StageBuilder stageBuilder = new StageBuilder();
            gliffyDiagram.Stage = stageBuilder
                .WithEaRepository(eaRepository)
                .WithEaDiagram(eaDiagram)
                .Build()
                .GetStage();
        }

        private void buildMetadata()
        {
            MetadataBuilder metadataBuilder = new MetadataBuilder();
            gliffyDiagram.Metadata = metadataBuilder
                .WithEaDiagram(eaDiagram)
                .Build()
                .GetMetadata();
        }
        #endregion
    }
}
