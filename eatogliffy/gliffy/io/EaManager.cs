using EA;
using eatogliffy.gliffy.builder;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace eatogliffy.gliffy.io
{
    public class EaManager
    {
        private Repository eaRepository;

        public EaManager openFile(string filePath)
        {
            closeFile();
            eaRepository = new Repository();
            eaRepository.OpenFile(filePath);
            return this;
        }

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

        public EaManager closeFile()
        {
            if(eaRepository != null)
            {
                eaRepository.CloseFile();
                eaRepository.Exit();
            }
            
            return this;
        }

        public EaManager selectDiagram(string diagramGuid)
        {
            Diagram selectedDiagram = eaRepository.GetDiagramByGuid(diagramGuid);
            if(selectedDiagram != null)
            {
                eaRepository.ActivateDiagram(selectedDiagram.DiagramID);
            }
            
            return this;
        }

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
                catch (Exception ex)
                {
                    throw;
                }
            }

            return String.Empty;
        }

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
