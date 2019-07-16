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

    class DiagramBuilderReference
    {
        public DiagramBuilderReference(string match, string diagramPrefix, string elementPrefix, IDiagramBuilder diagramBuilder)
        {
            Match = match;
            DiagramPrefix = diagramPrefix;
            ElementPrefix = elementPrefix;
            DiagramBuilderRef = diagramBuilder;
        }

        public string Match { get; set; }
        public string DiagramPrefix { get; set; }
        public string ElementPrefix { get; set; }
        public IDiagramBuilder DiagramBuilderRef { get; set; }
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
        private List<DiagramBuilderReference> diagramBuilderReferences = new List<DiagramBuilderReference>()
        {
            new DiagramBuilderReference("*", "D", "E", new DefaultDiagramBuilder()),
            new DiagramBuilderReference("fmea", "FD", "FE", new FmeaDiagramBuilder())
        };

        private List<string> blackListedElements = new List<string>() {
            "Text",
            "Boundary",
            "Note",
            "ProvidedInterface",
            "RequiredInterface",
            "ActivityPartition",
            "StateNode",
            "Rectangle",
            "Port"
        };

        private List<string> linkedElements = new List<string>() {
            "Package",
            "Activity",
            "UMLDiagram"
        };

        private const string TAG_HIDDEN = "hidden";

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

            // Initialize generators
            foreach(DiagramBuilderReference diagramBuilderReference in diagramBuilderReferences)
            {
                diagramBuilderReference.DiagramBuilderRef.Initialize(eaRepository, templateReader);
            }

            // Create package fragment first
            List<FragmentReference> referenceList = new List<FragmentReference>();
            referenceList.Add(
                new FragmentReference(
                    FragmentType.Package, 
                    getPackageContent(package)));

            // Generate fragment of each diagram
            foreach (Diagram diagram in package.Diagrams)
            {
                ImageReference imageReference = docWriter.CreateImageReference(diagram.DiagramID, diagram.Name);

                foreach (DiagramBuilderReference diagramBuilderReference in diagramBuilderReferences)
                {
                    if (diagramBuilderReference.Match.Equals("*") || diagram.Stereotype.Contains(diagramBuilderReference.Match))
                    {
                        string diagramContent = diagramBuilderReference.DiagramBuilderRef.GetBasicContent(package, diagram, imageReference);
                        referenceList.Add(
                            new FragmentReference(
                                package.Name.Equals(diagram.Name) ? FragmentType.DiagramUncaptioned : FragmentType.Diagram,
                                docWriter.WriteFragment(diagramBuilderReference.DiagramPrefix, diagram.DiagramID, diagram.Name, diagramContent)));

                        // Create fragments
                        string elementContent = diagramBuilderReference.DiagramBuilderRef.GetElementContent(package, diagram);

                        // Generate and store elements fragment
                        referenceList.Add(
                            new FragmentReference(
                                FragmentType.ElementList,
                                docWriter.WriteFragment(diagramBuilderReference.ElementPrefix, diagram.DiagramID, diagram.Name, elementContent)));
                    }
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
            return !(blackListedElements.Contains(element.Type) || element.Tag.Contains(TAG_HIDDEN));
        }

        private bool validateFmeaConnector(Connector connector)
        {
            return connector.MetaType.Equals("InformationFlow");
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
                sectionContent = sectionContent.Replace("{NOTES}", notes);
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
