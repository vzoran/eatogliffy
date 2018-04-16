using EA;
using eacore.io;
using EaToGliffy.Gliffy.Builder.Core;
using EaToGliffy.Gliffy.Model;
using Newtonsoft.Json;
using System;

namespace EaToGliffy.Gliffy.IO
{
    public class GliffyManager: EaManager
    {
        /// <summary>
        /// Converts a diagram to Gliffy format
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
                        .WithContentType(DiagramBuilder.DEFAULT_CONTENT_TYPE)
                        .WithVersion(DiagramBuilder.DEFAULT_VERSION)
                        .FromActiveDiagram(eaRepository)
                        .Build()
                        .GetDiagram();

                    var json = JsonConvert.SerializeObject(gliffyDiagram);

                    return json;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

            return String.Empty;
        }
    }
}
