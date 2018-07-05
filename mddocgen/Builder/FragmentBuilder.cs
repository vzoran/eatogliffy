﻿using EA;
using Html2Markdown;
using MdDocGenerator.IO;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace MdDocGenerator.Builder
{
    /// <summary>
    /// Class for generating MMD document fragment out of a single EA package
    /// </summary>
    public class FragmentBuilder
    {
        private IDocWriter docWriter;
        private ITemplateReader templateReader;
        private Repository eaRepository;

        /// <summary>
        /// Builder kind setter, in order to add a document writer
        /// </summary>
        /// <param name="docWriter">a non-null document writer implementation</param>
        /// <returns>Self reference</returns>
        public FragmentBuilder SetDocWriter(IDocWriter docWriter)
        {
            this.docWriter = docWriter;
            return this;
        }

        /// <summary>
        /// Builder kind setter, in order to add a template reader
        /// </summary>
        /// <param name="templateReader">a non-null template implementation</param>
        /// <returns>Self reference</returns>
        public FragmentBuilder SetTemplateReader(ITemplateReader templateReader)
        {
            this.templateReader = templateReader;
            return this;
        }

        /// <summary>
        /// Builder kind setter, in order to add a EA PAckage object for image dump
        /// </summary>
        /// <param name="eaRepository">a non-null EA's Repository object</param>
        /// <returns></returns>
        public FragmentBuilder SetEaRepository(Repository eaRepository)
        {
            this.eaRepository = eaRepository;
            return this;
        }

        /// <summary>
        /// Build the document fragment including its diagrams.
        /// </summary>
        /// <param name="package">source; non-null EA package.</param>
        /// <returns>References of the generated fragment</returns>
        public List<string> Build(Package package)
        {
            // TODO: Validate the setup and throw exception

            // Create package fragment first
            List<string> referenceList = new List<string>();
            referenceList.Add(getPackageContent(package));

            // Generate fragment of each diagram
            foreach (Diagram diagram in package.Diagrams)
            {
                referenceList.Add(getDiagramContent(package, diagram));

                // Get elementlist header
                StringBuilder elementContent = new StringBuilder();
                elementContent.Append(getDefaultContent(diagram.Name, String.Empty, templateReader.ReadTemplate(TemplateType.ElementHeader)));

                // Iterate elements and collect the references to add to the diagram template
                foreach (DiagramObject diagramObject in diagram.DiagramObjects)
                {
                    Element element = eaRepository.GetElementByID(diagramObject.ElementID);
                    if(validateElement(element))
                    {
                        elementContent.Append(getDefaultContent(element.Name, element.Notes, templateReader.ReadTemplate(TemplateType.Element)));
                    }

                    Console.WriteLine(String.Format("Element type: {0}; subtype: {1}", element.Type, element.Subtype));
                }

                // Generate and store elements fragment
                referenceList.Add(docWriter.WriteFragment("E", diagram.DiagramID, diagram.Name, elementContent.ToString()));
            }

            return referenceList;
        }

        private bool validateElement(Element element)
        {
            return !element.Type.Equals("Note");
        }

        private string getPackageContent(Package package)
        {
            string packageContent = getDefaultContent(package.Name, package.Notes, templateReader.ReadTemplate(TemplateType.Package));
            return docWriter.WriteFragment("P", package.PackageID, package.Name, packageContent);
        }

        private string getDiagramContent(Package parentPackage, Diagram diagram)
        {
            // Convert caption of this fragment
            string caption = diagram.Name.Equals(parentPackage.Name) ? "Overview" : diagram.Name;
            
            
            // Fill basic fields
            string diagramContent = getDefaultContent(caption, diagram.Notes, templateReader.ReadTemplate(TemplateType.Diagram));

            // Get diagram image reference in MMD document
            ImageReference imageReference = docWriter.CreateImageReference(diagram.DiagramID, diagram.Name);
            
            // Save image and store the reference
            this.eaRepository
                .GetProjectInterface()
                .PutDiagramImageToFile(diagram.DiagramGUID, imageReference.fullImagePath, 1);

            diagramContent = diagramContent.Replace("{DIAGRAM_IMAGE}", String.Format("![{0}][{1}]", diagram.Name, imageReference.imageID));

            return docWriter.WriteFragment("D", diagram.DiagramID, diagram.Name, diagramContent);
        }

        private string getDefaultContent(string name, string notes, string template)
        {
            string sectionContent = template;
            Converter mdConverter = new Converter();

            sectionContent = sectionContent.Replace("{NAME}", name);
            sectionContent = sectionContent.Replace("{NOTES}", mdConverter.Convert(notes));
            
            return sectionContent;
        }
    }
}
