﻿using eacore.io;
using MdDocGenerator.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator
{
    /// <summary>
    /// Main entry class for accessing EA objects
    /// </summary>
    public class MdDocManager : EaManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MdDocManager()
        {
        }

        /// <summary>
        /// Create full package of documentation
        /// </summary>
        /// <param name="sourceFile">full path of the source EA file</param>
        /// <param name="targetFolder">root target folder</param>
        public void GenerateMdDoc(string sourceFile, string targetFolder)
        {
            // Open the EA file
            this.OpenFile(sourceFile);

            // Generate the documentation package
            DocumentationBuilder documentationBuilder = new DocumentationBuilder();
            documentationBuilder
                .SetEaRepository(this.eaRepository)
                .SetTargetFolder(targetFolder)
                .Build();

            // Close EA file
            this.CloseFile();
        }
    }
}
