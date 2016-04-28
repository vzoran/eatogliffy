﻿using EA;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.model;
using System;

namespace eatogliffy.gliffy.builder
{
    public class DiagramBuilder
    {
        public static string DEFAULT_VERSION = "1.3";
        public static string DEFAULT_CONTENT_TYPE = "application/gliffy+json";
         
        private GliffyDiagram gliffyDiagram;
        private Diagram eaDiagram;
        private Repository eaRepository;

        public DiagramBuilder()
        {
            gliffyDiagram = new GliffyDiagram();
        }

        public DiagramBuilder withVersion(string version)
        {
            gliffyDiagram.version = version;
            return this;
        }

        public DiagramBuilder withContentType(string contentType)
        {
            gliffyDiagram.contentType = contentType;
            return this;
        }

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

        public DiagramBuilder build()
        {
            if(eaDiagram == null)
            {
                throw new NullReferenceException("No diagram selected");
            }

            IdManager.Initialize(eaRepository);

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

        public GliffyDiagram getDiagram()
        {
            return gliffyDiagram;
        }
    }
}
