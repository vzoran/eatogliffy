using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.tools
{
    public class LinkGeometry
    {
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int EndX { get; private set; }
        public int EndY { get; private set; }
        public eObjectSide Edge { get; private set; }

        /// <summary>
        /// Default constructor. Intentionally hidden.
        /// </summary>
        private LinkGeometry()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="GeometryString">Link geometry in format: "SX=0;SY=-5;EX=0;EY=11;EDGE=2;$LLB=;LLT=;LMT=;LMB=;LRT=;LRB=;IRHS=;ILHS=;"</param>
        public LinkGeometry (string geometryString)
        {
            string[] geoparams = geometryString.Split(new char[] { ';', '=' });
            for (int i = 0; i < geoparams.Length; i += 2)
            {
                switch(geoparams[i])
                {
                    case "SX":
                        StartX = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "SY":
                        StartY = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "EX":
                        EndX = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "EY":
                        EndY = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "EDGE":
                        Edge = (eObjectSide)Int32.Parse(geoparams[i + 1]);
                        break;
                }
            }
        }
    }

    public enum eObjectSide
    {
        Default = 0,
        Top = 1,
        Right = 2,
        Bottom = 3,
        Left = 4
    }
}
