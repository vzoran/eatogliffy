using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EA;
using MdDocGenerator.IO;
using MdDocGenerator.Template;

namespace MdDocGenerator.Builder
{
    class FmeaDiagramBuilder : IDiagramBuilder
    {
        private ITemplateReader templateReader;
        private Repository eaRepository;

        public string GetBasicContent(Package parentPackage, Diagram diagram, ImageReference imageReference)
        {
            Console.WriteLine("FMEA for Diagram {0} saved.", diagram.Name);

            return String.Empty;
        }

        public string GetElementContent(Package parentPackage, Diagram diagram)
        {
            Console.WriteLine("Element FMEA for Diagram {0} saved.", diagram.Name);

            return String.Empty;
        }

        public void Initialize(Repository repository, ITemplateReader templateReader)
        {
            eaRepository = repository;
            this.templateReader = templateReader;
        }
    }
}
