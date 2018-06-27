using EA;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Builder
{
    public abstract class DocumentationBuilder
    {
        protected ITemplateReader templateReader = new ResourceTemplateReader();
        protected SectionBuilder sectionBuilder = new SectionBuilder();
        protected string targetFolder = "\\";

        public DocumentationBuilder SetTemplateReader(ITemplateReader templateReader)
        {
            this.templateReader = templateReader;
            return this;
        }

        public DocumentationBuilder SetTargetFolder(string targetFolder)
        {
            this.targetFolder = targetFolder;
            return this;
        }

        protected virtual bool validatePackage(Package package)
        {
            return true;
        }

        private void parsePackage(Package package, int intend)
        {
            if(validatePackage(package))
            {
                string packageTemplate = templateReader.ReadTemplate(TemplateType.Package);
                string mdPackageDoc = sectionBuilder.BuildSection(package, packageTemplate, intend);

                // TODO: Save content to file
                // TODD: Save reference to master document
            }

            foreach(Package subpackage in package.Packages)
            {
                parsePackage(subpackage, intend + 1);
            }
        }

        public void Build(Repository repository)
        {
            foreach (Package model in repository.Models)
            {
                parsePackage(model, 0);
            }
        }
    }
}
