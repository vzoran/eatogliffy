using EA;
using MdDocGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Builder
{
    interface IDiagramBuilder
    {
        string GetBasicContent(Package parentPackage, Diagram diagram, string imageId);
        string GetElementContent(Package parentPackage, Diagram diagram);
    }
}
