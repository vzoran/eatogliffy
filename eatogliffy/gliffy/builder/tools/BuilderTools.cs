using EA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Tools
{
    /// <summary>
    /// Set of generic purpose Tools 
    /// </summary>
    public static class BuilderTools
    {
        public static readonly string COLOR_DEFAULT = "#FFFFFF";
        public static readonly string COLOR_BLACK = "#000000";

        /// <summary>
        /// Converts color codes from Integer to HTML encoding
        /// </summary>
        /// <param name="color">Integer color code</param>
        /// <returns>Color code in HTML format (e.g. #FFFFFF)</returns>
        public static string HexConverter(int color)
        {
            return HexConverter(color, null);
        }

        /// <summary>
        /// Converts color codes from Integer to HTML encoding
        /// </summary>
        /// <param name="color">Integer color code</param>
        /// <param name="overrideDefault">Replacement of the default color</param>
        /// <returns>Color code in HTML format (e.g. #FFFFFF)</returns>
        public static string HexConverter(int color, string overrideDefault)
        {
            var b = ((color >> 16) & 0xff);
            var g = ((color >> 8) & 0xff);
            var r = (color & 0xff);

            string colorStr = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");

            if (colorStr.Equals(COLOR_DEFAULT) && !String.IsNullOrEmpty(overrideDefault))
            {
                colorStr = overrideDefault;
            }

            return colorStr;
        }

        /// <summary>
        /// Returns with a given object of a diagram.
        /// </summary>
        /// <param name="diagram">Not null diagram object</param>
        /// <param name="diagramId">Id of the selected diagram object</param>
        /// <returns>Selected diagram object or null</returns>
        public static DiagramObject GetDiagramObjectById(Diagram diagram, string diagramId)
        {
            if(String.IsNullOrEmpty(diagramId) || diagram == null)
            {
                return null;
            }

            IEnumerator objectEnumerator = diagram.DiagramObjects.GetEnumerator();
            while (objectEnumerator.MoveNext())
            {
                DiagramObject diagramObject = (DiagramObject)objectEnumerator.Current;
                if(diagramId.Equals(diagramObject.InstanceGUID))
                {
                    return diagramObject;
                }
            }

            return null;
        }
    }
}
