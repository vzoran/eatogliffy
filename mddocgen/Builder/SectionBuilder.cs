using EA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Builder
{
    public class SectionBuilder
    {
        public string BuildSection(Object sourceObject, string template, int intend)
        {
            string sectionContent = String.Empty;

            if (sourceObject is Package)
            {
                return getPackageContent((Package)sourceObject, template, intend);
            }
            else if (sourceObject is Diagram)
            {
                return getDiagramContent((Diagram)sourceObject, template, intend);
            }
            else
            {
                return getDefaultContent(sourceObject, template, intend);
            }
        }

        private string getPackageContent(Package package, string template, int intend)
        {
            string packageContent = getDefaultContent(package, template, intend);

            // TODO: Fill diagram images

            return packageContent;
        }

        private string getDiagramContent(Diagram diagram, string template, int intend)
        {
            string diagramContent = getDefaultContent(diagram, template, intend);

            // TODO: Fill diagram images

            return diagramContent;
        }

        private string getDefaultContent(Object sourceObject, string template, int intend)
        {
            string sectionContent = template;

            sectionContent.Replace("{INTEND}", new String('=', intend));

            sectionContent.Replace("{NAME}", sourceObject.GetType().GetProperty("Name").GetValue(sourceObject).ToString());
            sectionContent.Replace("{NOTES}", sourceObject.GetType().GetProperty("Notes").GetValue(sourceObject).ToString());
            
            return "";
        }
    }
}
