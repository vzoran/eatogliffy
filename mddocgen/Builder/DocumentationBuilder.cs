using EA;
using eacore.io;
using MdDocGenerator.IO;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace MdDocGenerator.Builder
{
    public class DocumentationBuilder
    {
        private ITemplateReader templateReader = new ResourceTemplateReader();
        private FragmentBuilder fragmentBuilder = new FragmentBuilder();
        private IDocWriter docWriter;
        private Repository eaRepository;
        private string title;

        public DocumentationBuilder SetTitle(string title)
        {
            this.title = title;
            return this;
        }

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
            return !package.IsModel;
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
                List<FragmentReference> mdPackageReferences = fragmentBuilder
                    .SetEaRepository(eaRepository)
                    .SetDocWriter(docWriter)
                    .SetTemplateReader(templateReader)
                    .Build(package);

                // Store it in master document
                int cnt = 0;
                int realIntendation = 0;
                foreach(FragmentReference refLine in mdPackageReferences)
                {

                    switch(refLine.fragmentType)
                    {
                        case FragmentType.Diagram:
                            // diagrams starts one level lower
                            realIntendation = intend + 1;
                            break;

                        case FragmentType.ElementList:
                            // elements of a diagram starts 2 levels lower
                            realIntendation = intend + 2;
                            break;

                        case FragmentType.Package:
                        default:
                            // use given intend by default and paragraphs
                            realIntendation = intend;
                            break;
                    }
                    
                    // save reference
                    docWriter.WriteToMasterDoc(refLine.reference, true, realIntendation);
                    cnt++;
                }

                // increment intendation if package added to the document
                intend++;
            }

            foreach(Package subpackage in package.Packages)
            {
                parsePackage(subpackage, intend);
            }
        }

        private string formatAuthors(Collection authors, string separator)
        {
            StringBuilder authList = new StringBuilder();

            for (short i = 0; i < authors.Count; ++i)
            {
                if(i != 0)
                {
                    authList.Append(separator);
                }
                authList.Append(((Author)authors.GetAt(i)).Name);
            }

            return authList.ToString();
        }

        public void Build()
        {
            // Fill header values
            printHeaders();

            // Parse all models
            foreach (Package model in eaRepository.Models)
            {
                if(validateModel(model))
                {
                    parsePackage(model, 1);
                }
            }

            docWriter.FinalizeMaster();
        }

        private void printHeaders()
        {
            docWriter.AddMetaInfo("Title", (String.IsNullOrEmpty(title) ? "EA documentation" : title));
            docWriter.AddMetaInfo("Author", formatAuthors(eaRepository.Authors, "; "));
            docWriter.AddMetaInfo("Comment", "This document is created by MMD generator.");
            docWriter.AddMetaInfo("Date", DateTime.Now.ToString("d MMM yyyy"));
            // TODO: copy master.css
            docWriter.AddMetaInfo("CSS", "./master.css");
            docWriter.AddMetaInfo("Format", "complete");
            docWriter.WriteToMasterDoc(Environment.NewLine, false);

            docWriter.WriteToMasterDoc("# Table of content" + Environment.NewLine, false);
            docWriter.WriteToMasterDoc("{{TOC}}" + Environment.NewLine, false);

            docWriter.WriteToMasterDoc(Environment.NewLine, false);
        }
    }
}
