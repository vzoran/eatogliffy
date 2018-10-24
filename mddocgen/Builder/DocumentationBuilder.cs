using EA;
using eacore.io;
using MdDocGenerator.IO;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace MdDocGenerator.Builder
{
    /// <summary>
    /// Class for generating documentation package out of an EA repository
    /// </summary>
    public class DocumentationBuilder
    {
        private ITemplateReader templateReader = new ResourceTemplateReader();
        private FragmentBuilder fragmentBuilder = new FragmentBuilder();
        private IDocWriter docWriter;
        private Repository eaRepository;
        private BuilderConfig builderConfig = new BuilderConfig();

        /// <summary>
        /// Setter of target folder
        /// </summary>
        /// <param name="targetFolder">Location of the documentation package</param>
        /// <returns>Self reference</returns>
        public DocumentationBuilder SetTargetFolder(string targetFolder)
        {
            docWriter = new DocumentationFileWriter(targetFolder);
            docWriter.Initialize();
            return this;
        }

        /// <summary>
        /// Setter of the EA repository
        /// </summary>
        /// <param name="eaRepository">A non-null instance of an EA repository</param>
        /// <returns>Self reference</returns>
        public DocumentationBuilder SetEaRepository(Repository eaRepository)
        {
            this.eaRepository = eaRepository;
            return this;
        }

        /// <summary>
        /// Setter for a template reader.
        /// </summary>
        /// <param name="templateReader">A template reader instance</param>
        /// <returns></returns>
        public DocumentationBuilder SetTemplateReader(ITemplateReader templateReader)
        {
            this.templateReader = templateReader;
            return this;
        }

        /// <summary>
        /// Setter for the configuration
        /// </summary>
        /// <param name="builderConfig"></param>
        /// <returns></returns>
        public DocumentationBuilder SetConfiguration(BuilderConfig builderConfig)
        {
            this.builderConfig = builderConfig;
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
                List<FragmentReference> mdPackageReferences = fragmentBuilder.Build(package);

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

        /// <summary>
        /// Build documentation package based on the configuration
        /// </summary>
        public void Build()
        {
            // Set builder up
            fragmentBuilder
                .SetEaRepository(eaRepository)
                .SetDocWriter(docWriter)
                .SetTemplateReader(templateReader);

            // Fill header values
            printHeaders();

            // Save generic fragments
            printAbbreviations();

            // Parse all models
            foreach (Package model in eaRepository.Models)
            {
                if(validateModel(model))
                {
                    parsePackage(model, 1);
                }
            }

            // Finalize
            docWriter.FinalizeMaster();
        }

        private void printHeaders()
        {
            docWriter.AddMetaInfo("Title", builderConfig.Title);
            docWriter.AddMetaInfo("Author", formatAuthors(eaRepository.Authors, "; "));
            docWriter.AddMetaInfo("Comment", "This document is created by MMD generator.");
            docWriter.AddMetaInfo("Date", DateTime.Now.ToString("d MMM yyyy"));
            // TODO: copy master.css
            docWriter.AddMetaInfo("CSS", builderConfig.CssTemplate);
            docWriter.AddMetaInfo("Format", "complete");
            docWriter.WriteToMasterDoc(Environment.NewLine, false);

            if(this.builderConfig.TocIncluded)
            {
                docWriter.WriteToMasterDoc("# Table of content" + Environment.NewLine, false);
                docWriter.WriteToMasterDoc("{{TOC}}" + Environment.NewLine, false);
                docWriter.WriteToMasterDoc(Environment.NewLine, false);
            }
        }

        private void printAbbreviations()
        {
            // Build fragment of abbreviations
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(fragmentBuilder.getContentByType(String.Empty, String.Empty, TemplateType.AbbreviationHeader));

            // Get and sort abbreviations
            List<Term> abbrList = new List<Term>();
            foreach (Term term in eaRepository.Terms)
            {
                abbrList.Add(term);
            }
            abbrList.Sort(delegate (Term x, Term y)
            {
                return x.Term.CompareTo(y.Term);
            });

            // Print sorted list
            foreach (Term term in abbrList)
            {
                stringBuilder.AppendLine(fragmentBuilder.getContentByType(term.Term, term.Meaning, TemplateType.Abbreviation, true));
            }

            // Save it to file
            string reference = docWriter.WriteFragment("A", 1, "abbreviation", stringBuilder.ToString());

            // Add reference to the master document
            docWriter.WriteToMasterDoc(reference, true, 1);
        }
    }
}
