using EA;
using eatogliffy.gliffy.builder.tools;
using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.graphics
{
    public class LineBuilder
    {
        private DiagramLink eaDiagramLink;
        private Connector eaConnector;
        private eLineType lineType;
        private GliffyGraphicLine gliffyLine;
        

        public LineBuilder withEaConnector(Connector connector)
        {
            this.eaConnector = connector;
            return this;
        }

        public LineBuilder withEaLink(DiagramLink diagramLink)
        {
            this.eaDiagramLink = diagramLink;
            return this;
        }

        public LineBuilder withType(eLineType lineType)
        {
            this.lineType = lineType;
            return this;
        }

        public LineBuilder build()
        {
            gliffyLine = new GliffyGraphicLine();
            gliffyLine.Line = new GliffyLine();

            gliffyLine.Line.strokeColor = BuilderTools.hexConverter(eaDiagramLink.LineColor, BuilderTools.COLOR_BLACK);
            gliffyLine.Line.startArrowRotation = "auto";
            gliffyLine.Line.endArrowRotation = "auto";
            gliffyLine.Line.interpolationType = "linear";

            return this;
        }

        public GliffyGraphicLine getLine()
        {
            return gliffyLine;
        }
    }

    public enum eLineType
    {
        Dependency
    }
}
