using EA;
using MdDocGenerator.IO;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;

namespace MdDocGenerator.Builder
{
    public class DocumentationBuilder
    {
        private ITemplateReader templateReader = new ResourceTemplateReader();
        private FragmentBuilder fragmentBuilder = new FragmentBuilder();
        private IDocWriter docWriter;
        private Repository eaRepository;

        public DocumentationBuilder SetTargetFolder(string targetFolder)
        {
            docWriter = new DocumentationFileWriter(targetFolder);
            docWriter.Initialize();
            return this;
        }

        public DocumentationBuilder SetEaRepository(Repository eaRepository)
        {
            this.eaRepository = eaRepository;
            return this;
        }

        public DocumentationBuilder SetTemplateReader(ITemplateReader templateReader)
        {
            this.templateReader = templateReader;
            return this;
        }

        protected virtual bool validatePackage(Package package)
        {
            return true;
        }

        protected virtual bool validateModel(Package package)
        {
            return true;
        }

        private void parsePackage(Package package, int intend)
        {
            if (validatePackage(package))
            {
                // Create package document fragments
                List<string> mdPackageReferences = fragmentBuilder
                    .SetEaRepository(eaRepository)
                    .SetDocWriter(docWriter)
                    .SetTemplateReader(templateReader)                    
                    .Build(package);

                // Store it in master document
                int cnt = 0;
                foreach(string refLine in mdPackageReferences)
                {
                    docWriter.WriteToMasterDoc(refLine, true, (cnt == 0 ? intend : 0));
                    cnt++;
                }
            }

            foreach(Package subpackage in package.Packages)
            {
                parsePackage(subpackage, intend + 1);
            }
        }

        public void Build()
        {
            foreach (Package model in eaRepository.Models)
            {
                if(validateModel(model))
                {
                    parsePackage(model, 1);
                }
            }

            docWriter.FinalizeMaster();
        }
    }
}
