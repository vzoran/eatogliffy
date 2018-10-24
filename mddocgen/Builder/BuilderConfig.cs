using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.Builder
{
    /// <summary>
    /// Class for configuring a document generation process
    /// </summary>    
    public class BuilderConfig
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BuilderConfig()
        {
            TocIncluded = false;
            Title = "EA documentation";
            CssTemplate = "./master.css";
        }

        /// <summary>
        /// Should it generate table of contents?
        /// </summary>
        public bool TocIncluded { get; set; }

        /// <summary>
        /// Title of the generated document
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// CSS template used by HTML generator
        /// </summary>
        public string CssTemplate { get; set; }
    }
}
