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

        private void parsePackage(Package package, int intend)
        {
            if (validatePackage(package))
            {
                List<string> mdPackageReferences = fragmentBuilder
                    .SetEaPackage(eaProject)
                    .SetDocWriter(docWriter)
                    .SetTemplateReader(templateReader)                    
                    .Build(package);

                // TODD: Save reference to master document
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
                parsePackage(model, 0);
            }
        }
    }
}
