using EA;
using MdDocGenerator.IO;
using MdDocGenerator.Template;

namespace MdDocGenerator.Builder
{
    public interface IDiagramBuilder
    {
        void Initialize(Repository repository, ITemplateReader templateReader);
        string GetBasicContent(Package parentPackage, Diagram diagram, ImageReference imageReference);
        string GetElementContent(Package parentPackage, Diagram diagram);
    }
}
