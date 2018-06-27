using MdDocGenerator.Builder;
using System.IO;
using System.Reflection;

namespace MdDocGenerator.Template
{
    public class ResourceTemplateReader: ITemplateReader
    {
        public string ReadTemplate(TemplateType templateType)
        {
            string resourceName = "mddocgen.";
            switch (templateType)
            {
                case TemplateType.Package:
                    resourceName += "package_md.txt";
                    break;

                case TemplateType.Diagram:
                    resourceName += "diagram_md.txt";
                    break;

                default:
                    resourceName += "default_md.txt";
                    break;
            }

            var assembly = Assembly.GetExecutingAssembly();
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
