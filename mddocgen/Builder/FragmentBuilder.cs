using EA;
using eacore.io;
using Html2Markdown;
using MdDocGenerator.IO;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace MdDocGenerator.Builder
{
    public enum FragmentType
    {
        Other,
        Package,
        Diagram,
        ElementList,
        DiagramUncaptioned
    }

    /// <summary>
    /// Definition of a fragment
    /// </summary>
    public struct FragmentReference
    {
        public FragmentType fragmentType;
        public string reference;
  

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fragmentType">Type of the fragment</param>
        /// <param name="reference">Reference text</param>
        public FragmentReference(FragmentType fragmentType, string reference)
        {
            this.fragmentType = fragmentType;
            this.reference = reference;
        }
    }

    /// <summary>
    /// Class for generating MMD document fragment out of a single EA package
    /// </summary>
    public class FragmentBuilder
    {
        private IDocWriter docWriter;
        private ITemplateReader templateReader;
        private Repository eaRepository;
        private BuilderConfig builderConfig;

        private List<string> blackListedElements = new List<string>() {
            "Text",
            "Boundary",
            "Note",
            "ProvidedInterface",
            "RequiredInterface",
            "ActivityPartition",
            "StateNode"
        };

        private List<string> linkedElements = new List<string>() {
            "Package",
            "Activity",
            "UMLDiagram"
        };

        /// <summary>
        /// Builder kind setter, in order to add a document writer
        /// </summary>
        /// <param name="docWriter">a non-null document writer implementation</param>
        /// <returns>Self reference</returns>
        public FragmentBuilder SetDocWriter(IDocWriter docWriter, BuilderConfig builderConfig)
        {
            this.docWriter = docWriter;
            this.builderConfig = builderConfig;
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
        public List<FragmentReference> Build(Package package)
        {
            // TODO: Validate the setup and throw exception

            // Create package fragment first
            List<FragmentReference> referenceList = new List<FragmentReference>();
            referenceList.Add(
                new FragmentReference(
                    FragmentType.Package, 
                    getPackageContent(package)));

            // Generate fragment of each diagram
            foreach (Diagram diagram in package.Diagrams)
            {   
                referenceList.Add(
                    new FragmentReference(
                        package.Name.Equals(diagram.Name) ? FragmentType.DiagramUncaptioned : FragmentType.Diagram, 
                        getDiagramContent(package, diagram)));
                
                // Iterate elements and collect the references to add to the diagram template
                List<Element> elementList = new List<Element>();
                bool elementsUpdated = false;
                foreach (DiagramObject diagramObject in diagram.DiagramObjects)
                {
                    Element element = eaRepository.GetElementByID(diagramObject.ElementID);
                    if(validateElement(element))
                    {
                        elementList.Add(element);

                        // Check whether it has changed or not
                        if (builderConfig.CleanRun || builderConfig.LastRun <= element.Modified)
                        {
                            elementsUpdated = true;
                            Console.WriteLine(String.Format("Element {0} saved", element.Name));
                        } else
                        {
                            Console.WriteLine(String.Format("Element {0} skipped", element.Name));
                        }
                    }
                }

                // Create fragment only if it makes sense
                if (elementsUpdated && elementList.Count > 0)
                {
                    elementList.Sort(delegate (Element x, Element y)
                    {
                        int typeCompare = x.Type.CompareTo(y.Type);
                        if (typeCompare == 0)
                        {
                            return x.Name.CompareTo(y.Name);
                        }

                        return typeCompare;
                    });

                    // Get elementlist header
                    StringBuilder elementContent = new StringBuilder();
                    elementContent.Append(getDefaultContent(diagram.Name, String.Empty, templateReader.ReadTemplate(TemplateType.ElementHeader)));

                    // generate element lines
                    foreach (Element element in elementList)
                    {
                        elementContent.Append(getElementContent(element));
                    }

                    // Generate and store elements fragment
                    referenceList.Add(
                        new FragmentReference(
                            FragmentType.ElementList,
                            docWriter.WriteFragment("E", diagram.DiagramID, diagram.Name, elementContent.ToString())));
                }
            }

            return referenceList;
        }

        /// <summary>
        /// Checks whether the element is allowed to put to he element's list
        /// </summary>
        /// <param name="element">a non-null element object</param>
        /// <returns>Allowed or not</returns>
        private bool validateElement(Element element)
        {
            return !blackListedElements.Contains(element.Type);
        }

        /// <summary>
        /// Generate MMD item out of a given element
        /// </summary>
        /// <param name="element">a non-null element object</param>
        /// <returns>Generated MMD definition in string format</returns>
        private string getElementContent(Element element)
        {
            bool isLinked = builderConfig.UseLinks && linkedElements.Contains(element.Type);
            string elementContent = getDefaultContent((isLinked ? createLink(element.Name) : element.Name), element.Notes, templateReader.ReadTemplate(TemplateType.Element), true);

            elementContent = elementContent.Replace("{TYPE}", element.Type);

            return elementContent;
        }

        /// <summary>
        /// Generate MMD item out of a given package
        /// </summary>
        /// <param name="package">a non-null package object</param>
        /// <returns>Generated MMD definition in string format</returns>
        private string getPackageContent(Package package)
        {
            string packageContent = null;
            if (builderConfig.CleanRun || builderConfig.LastRun <= package.Modified)
            {
                packageContent = getDefaultContent(package.Name, package.Notes, templateReader.ReadTemplate(TemplateType.Package));
            }
            
            return docWriter.WriteFragment("P", package.PackageID, package.Name, packageContent);
        }

        /// <summary>
        /// Generate MMD item out of a given diagram
        /// </summary>
        /// <param name="package">a non-null diagram object</param>
        /// <returns>Generated MMD definition in string format</returns>
        private string getDiagramContent(Package parentPackage, Diagram diagram)
        {
            string diagramContent = null;
            
            // Get diagram image reference in MMD document
            ImageReference imageReference = docWriter.CreateImageReference(diagram.DiagramID, diagram.Name);

            if (builderConfig.CleanRun || builderConfig.LastRun <= diagram.ModifiedDate)
            {
                // Convert caption of this fragment
                string caption = diagram.Name.Equals(parentPackage.Name) ? String.Empty : diagram.Name;

                // Fill basic fields
                diagramContent = getDefaultContent(caption, diagram.Notes, templateReader.ReadTemplate(TemplateType.Diagram));

                // Save image and store the reference
                this.eaRepository
                    .GetProjectInterface()
                    .PutDiagramImageToFile(diagram.DiagramGUID, imageReference.fullImagePath, 1);

                diagramContent = diagramContent.Replace("{DIAGRAM_IMAGE}", String.Format("![{0}][{1}]", diagram.Name, imageReference.imageID));
                Console.WriteLine("Diagram {0} saved.", diagram.Name);
            } else {
                Console.WriteLine("Diagram {0} skipped.", diagram.Name);
            }

            return docWriter.WriteFragment("D", diagram.DiagramID, diagram.Name, diagramContent);
        }

        /// <summary>
        /// Fills generic fields with values
        /// </summary>
        /// <param name="name">name value</param>
        /// <param name="notes">note value</param>
        /// <param name="template">template string </param>
        /// <param name"isSingleLine">needed to convert a single line note</param>
        /// <returns>template string with replaced field values</returns>
        private string getDefaultContent(string name, string notes, string template, bool isSingleLine = false)
        {
            string sectionContent = template;
            Converter mdConverter = new Converter();

            sectionContent = sectionContent.Replace("{NAME}", name);

            if(isSingleLine)
            {
                sectionContent = sectionContent.Replace("{NOTES}", notes.Replace(Environment.NewLine, " "));
            } 
            else
            {
                sectionContent = sectionContent.Replace("{NOTES}", mdConverter.Convert(notes).Replace(Environment.NewLine, "  " + Environment.NewLine));
            }
            
            return sectionContent;
        }

        /// <summary>
        /// Return with the template content filled woth default data
        /// </summary>
        /// <param name="name">name value</param>
        /// <param name="notes">note value</param>
        /// <param name="templateType">Type of the template we  want to read</param>
        /// <param name="isSingleLine">needed to convert a single line note</param>
        /// <returns></returns>
        public string getContentByType(string name, string notes, TemplateType templateType, bool isSingleLine = false)
        {
            string sectionContent = templateReader.ReadTemplate(templateType);
            return getDefaultContent(name, notes, sectionContent, isSingleLine);
        }

        /// <summary>
        /// Create an MMD style link
        /// </summary>
        /// <param name="text">Link text</param>
        /// <returns>Generate link in string format</returns>
        private string createLink(string text)
        {
            return String.Format("[{0}][]", text);
        }
    }
}
