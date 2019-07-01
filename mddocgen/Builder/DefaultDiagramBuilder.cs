using EA;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Builder
{
    class DefaultDiagramBuilder : IDiagramBuilder
    {
        private ITemplateReader templateReader;
        private Repository eaRepository;

        public IDiagramBuilder SetRepository(Repository repository)
        {
            this.eaRepository = repository;
            return this;
        }

        public IDiagramBuilder SetTemplateReader(ITemplateReader templateReader)
        {
            this.templateReader = templateReader;
            return this;
        }

        public string GetBasicContent(Package parentPackage, Diagram diagram, string imageId)
        {
            string diagramContent = null;

            // Convert caption of this fragment
            string caption = diagram.Name.Equals(parentPackage.Name) ? String.Empty : diagram.Name;

            // Fill basic fields
            diagramContent = templateReader.ReadTemplate(TemplateType.Diagram);
            diagramContent = diagramContent.Replace("{NAME}", diagram.Name);
            diagramContent = diagramContent.Replace("{NOTES}", diagram.Notes.Replace(Environment.NewLine, "<BR>"));
            diagramContent = diagramContent.Replace("{DIAGRAM_IMAGE}", String.Format("![{0}][{1}]", diagram.Name, imageId));

            return diagramContent;
        }

        public string GetElementContent(Package parentPackage, Diagram diagram)
        {
            StringBuilder elementContent = new StringBuilder();
            string elementLine = templateReader.ReadTemplate(TemplateType.ElementHeader);
            elementLine = elementLine.Replace("{NAME}", diagram.Name);
            elementContent.Append(elementLine);

            foreach (DiagramObject diagramObject in diagram.DiagramObjects)
            {
                Element element = eaRepository.GetElementByID(diagramObject.ElementID);
                
                elementLine = templateReader.ReadTemplate(TemplateType.Element);
                elementLine = elementLine.Replace("{NAME}", element.Name);
                elementLine = elementLine.Replace("{NOTES}", element.Notes.Replace(Environment.NewLine, "<BR>"));

                elementContent.Append(elementLine);
            }

            return elementContent.ToString();
        }
    }
}
