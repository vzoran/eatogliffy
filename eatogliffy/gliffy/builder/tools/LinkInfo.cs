using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.tools
{
    public class LinkInfo
    {
        public DiagramCoordinate Start;
        public DiagramCoordinate End;
        public eObjectSide Edge { get; private set; }
        public eLinkLineMode LineMode { get; private set; }

        /// <summary>
        /// Helper property to decide whether the line goes directly to the end or not.
        /// </summary>
        public bool IsStraight
        {
            get
            {
                return LineMode == eLinkLineMode.Direct;
            }
        }

        /// <summary>
        /// Default constructor. Intentionally hidden.
        /// </summary>
        private LinkInfo()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="diagramLink"></param>
        public LinkInfo (EA.DiagramLink diagramLink)
        {
            this.Start = new DiagramCoordinate();
            this.End = new DiagramCoordinate();

            parseGeometry(diagramLink.Geometry);
            parseStyle(diagramLink.Style);
        }

        /// <summary>
        /// Parser for Style property of a given DiagramLink
        /// </summary>
        /// <param name="linkStyle">Style string in format: "Mode=1;EOID=F46C55FA;SOID=D811935C;Color=-1;LWidth=0;" </param>
        private void parseStyle(string linkStyle)
        {
            string[] linkParams = linkStyle.Split(new char[] { ';', '=' });
            for (int i = 0; i < linkParams.Length; i += 2)
            {
                switch (linkParams[i])
                {
                    case "Mode":
                        LineMode = (eLinkLineMode)Int32.Parse(linkParams[i + 1]);
                        break;
                }
            }
        }

        /// <summary>
        /// Parser for Geometry property of a given DiagramLink
        /// </summary>
        /// <param name="geometryString">Link geometry in format: "SX=0;SY=-5;EX=0;EY=11;EDGE=2;$LLB=;LLT=;LMT=;LMB=;LRT=;LRB=;IRHS=;ILHS=;"</param>
        private void parseGeometry(string geometryString)
        {
            string[] geoparams = geometryString.Split(new char[] { ';', '=' });
            for (int i = 0; i < geoparams.Length; i += 2)
            {
                switch (geoparams[i])
                {
                    case "SX":
                        Start.PointX = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "SY":
                        Start.PointY = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "EX":
                        End.PointX = Int32.Parse(geoparams[i + 1]);
                        break;
                    case "EY":
                        End.PointY = Int32.Parse(geoparams[i + 1]);
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

    public enum eLinkLineMode
    {
        Default = -1,
        Direct = 1,
        Orthogonal = 3
    }
}
