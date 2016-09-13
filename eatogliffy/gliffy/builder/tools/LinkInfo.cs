using System;

namespace EaToGliffy.Gliffy.Builder.Tools
{
    /// <summary>
    /// Class for extracting diagram line characteristics.
    /// </summary>
    public class LinkInfo
    {
        public DiagramCoordinate Start { get; private set; }
        public DiagramCoordinate End { get; private set; }
        public eObjectSide Edge { get; private set; }
        public eLinkLineMode LineMode { get; private set; }
        public string Path { get; private set; }

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
            this.Path = diagramLink.Path;

            ParseGeometry(diagramLink.Geometry);
            ParseStyle(diagramLink.Style);
        }

        /// <summary>
        /// Parser for Style property of a given DiagramLinks
        /// </summary>
        /// <param name="linkStyle">Style string in format: "Mode=1;EOID=F46C55FA;SOID=D811935C;Color=-1;LWidth=0;" </param>
        private void ParseStyle(string linkStyle)
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
        /// Parser for Geometry property of a given DiagramLinks
        /// </summary>
        /// <param name="geometryString">Link geometry in format: "SX=0;SY=-5;EX=0;EY=11;EDGE=2;$LLB=;LLT=;LMT=;LMB=;LRT=;LRB=;IRHS=;ILHS=;"</param>
        private void ParseGeometry(string geometryString)
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

    /// <summary>
    /// Definition of object sides
    /// </summary>
    public enum eObjectSide
    {
        Default = 0,
        Top = 1,
        Right = 2,
        Bottom = 3,
        Left = 4
    }

    /// <summary>
    /// Definition of characteristics of a diagram link
    /// </summary>
    public enum eLinkLineMode
    {
        Default = -1,
        Direct = 1,
        Orthogonal = 3
    }
}
