using EA;
using eatogliffy.gliffy.builder.core;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace eatogliffy.gliffy.io
{
    /// <summary>
    /// Class for opening and querying an EA project file
    /// </summary>
    public class EaManager
    {
        private Repository eaRepository;

        /// <summary>
        /// Open a valid EA project
        /// </summary>
        /// <param name="filePath">Full path of the EA file</param>
        /// <returns>Return a self reference</returns>
        public EaManager openFile(string filePath)
        {
            closeFile();
            eaRepository = new Repository();
            eaRepository.OpenFile(filePath);
            return this;
        }

        /// <summary>
        /// Collects a list of folders and diagrams
        /// </summary>
        /// <returns>List of elements</returns>
        public List<EaObject> getDiagramList()
        {
            List<EaObject> results = new List<EaObject>();
            
            foreach(Package category in eaRepository.Models)
            {
                results.Add(new EaObject(category.PackageGUID, category.Name, null, false));

                foreach (Package package in category.Packages)
                {
                    collectDiagrams(results, category.PackageGUID, package);
                }
            }

            return results;
        }

        /// <summary>
        /// Remove lock from the EA project files
        /// </summary>
        /// <returns>Self reference</returns>
        public EaManager closeFile()
        {
            if(eaRepository != null)
            {
                eaRepository.CloseFile();
                eaRepository.Exit();
            }
            
            return this;
        }

        /// <summary>
        /// Mark a valid diagram as selected
        /// </summary>
        /// <param name="diagramGuid">Valid ID of the selected diagram</param>
        /// <returns>Self reference</returns>
        public EaManager selectDiagram(string diagramGuid)
        {
            Diagram selectedDiagram = eaRepository.GetDiagramByGuid(diagramGuid);
            if(selectedDiagram != null)
            {
                eaRepository.ActivateDiagram(selectedDiagram.DiagramID);
            }
            
            return this;
        }

        /// <summary>
        /// Converts a diagram to gliffy format
        /// </summary>
        /// <param name="diagramGuid">Valid ID of the selected diagram</param>
        /// <returns>Converted diagram in Gliffy's JSON format</returns>
        public string ConvertDiagram(string diagramGuid)
        {
            Diagram selectedDiagram = eaRepository.GetDiagramByGuid(diagramGuid);
            if (selectedDiagram != null)
            {
                eaRepository.OpenDiagram(selectedDiagram.DiagramID);
                eaRepository.ActivateDiagram(selectedDiagram.DiagramID);

                try
                {
                    DiagramBuilder diagramBuilder = new DiagramBuilder();
                    GliffyDiagram gliffyDiagram = diagramBuilder
                        .withContentType(DiagramBuilder.DEFAULT_CONTENT_TYPE)
                        .withVersion(DiagramBuilder.DEFAULT_VERSION)
                        .fromActiveDiagram(eaRepository)
                        .build()
                        .getDiagram();

                    var json = new JavaScriptSerializer()
                        .Serialize(gliffyDiagram);

                    return json;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Recursive function to collect all diagrams and folders in variable depth
        /// </summary>
        /// <param name="results">Reference of the result list</param>
        /// <param name="parentGuid">Unique ID of the parent folder</param>
        /// <param name="package">Parent package</param>
        private void collectDiagrams(List<EaObject> results, string parentGuid, Package package)
        {
            results.Add(new EaObject(package.PackageGUID, package.Name, parentGuid, false));

            foreach (Diagram diagram in package.Diagrams)
            {
                results.Add(new EaObject(diagram.DiagramGUID, diagram.Name, package.PackageGUID, true));
            }

            foreach(Package pack in package.Packages)
            {
                collectDiagrams(results, package.PackageGUID, pack);
            }
        }
    }
}
