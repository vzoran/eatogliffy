using eatogliffy.gliffy.builder.graphics;
using eatogliffy.gliffy.model.graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.diagramlink
{
    public class DependecyBuilder : LinkBuilder
    {
        protected override void buildProperties()
        {
            base.buildProperties();
            this.gliffyLink.uid = "com.gliffy.shape.uml.uml_v2.class.dependency";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();

            LineBuilder lineBuilder = new LineBuilder();
            GliffyGraphicLine line = lineBuilder
                .withEaRepository(eaRepository)
                .withEaConnector(eaConnector)
                .withEaLink(this.eaDiagramLink)
                .withType(eLineType.Dependency)
                .withLinkPosition(new tools.DiagramCoordinate(this.gliffyLink.x, this.gliffyLink.y))
                .build()
                .getLine();

            line.Line.dashStyle = "8.0,2.0";
            line.Line.endArrow = 6;
            line.Line.startArrow = 0;

            this.gliffyLink.graphic = line;
        }

        protected override void buildConstraints()
        {
            base.buildConstraints();


        }
    }
}
