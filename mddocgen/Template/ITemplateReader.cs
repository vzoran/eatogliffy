using MdDocGenerator.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Template
{
    public interface ITemplateReader
    {
        string ReadTemplate(TemplateType templateType);
    }
}
