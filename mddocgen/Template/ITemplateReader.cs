using MdDocGenerator.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Template
{
    /// <summary>
    /// Interface for reading element templates
    /// </summary>
    public interface ITemplateReader
    {
        /// <summary>
        ///  Read template string of a particular EA object
        /// </summary>
        /// <param name="templateType">Type iof the EA object</param>
        /// <returns>Template content</returns>
        string ReadTemplate(TemplateType templateType);
    }
}
