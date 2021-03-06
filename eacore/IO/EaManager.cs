﻿using EA;
using System;
using System.Collections.Generic;

namespace eacore.io
{
    /// <summary>
    /// Class for opening and querying an EA project file
    /// </summary>
    public class EaManager
    {
        protected Repository eaRepository;

        /// <summary>
        /// Open a valid EA project
        /// </summary>
        /// <param name="filePath">Full Path of the EA file</param>
        /// <returns>Return a self reference</returns>
        public EaManager OpenFile(string filePath)
        {
            CloseFile();
            eaRepository = new Repository();
            eaRepository.OpenFile(filePath);
            return this;
        }

        /// <summary>
        /// Collects a list of folders and diagrams
        /// </summary>
        /// <returns>List of elements</returns>
        public List<EaObject> GetDiagramList()
        {
            List<EaObject> results = new List<EaObject>();
            
            foreach(Package category in eaRepository.Models)
            {
                results.Add(new EaObject(category.PackageGUID, category.Name, null, false));

                foreach (Package package in category.Packages)
                {
                    CollectDiagrams(results, category.PackageGUID, package);
                }
            }

            return results;
        }

        /// <summary>
        /// Remove lock from the EA project files
        /// </summary>
        /// <returns>Self reference</returns>
        public EaManager CloseFile()
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
        public EaManager SelectDiagram(string diagramGuid)
        {
            Diagram selectedDiagram = eaRepository.GetDiagramByGuid(diagramGuid);
            if(selectedDiagram != null)
            {
                eaRepository.ActivateDiagram(selectedDiagram.DiagramID);
            }
            
            return this;
        }

        /// <summary>
        /// Recursive function to collect all diagrams and folders in variable depth
        /// </summary>
        /// <param name="results">Reference of the result list</param>
        /// <param name="parentGuid">Unique ID of the parent folder</param>
        /// <param name="package">Parent package</param>
        protected void CollectDiagrams(List<EaObject> results, string parentGuid, Package package)
        {
            results.Add(new EaObject(package.PackageGUID, package.Name, parentGuid, false));

            foreach (Diagram diagram in package.Diagrams)
            {
                results.Add(new EaObject(diagram.DiagramGUID, diagram.Name, package.PackageGUID, true));
            }

            foreach(Package pack in package.Packages)
            {
                CollectDiagrams(results, package.PackageGUID, pack);
            }
        }
    }
}
