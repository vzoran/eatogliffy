using EaToGliffy.Gliffy.Model.Graphics;

namespace EaToGliffy.Gliffy.Builder.DiagramLinks
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
            GliffyGraphicLine line = this.gliffyLink.graphic as GliffyGraphicLine;

            if(line != null && line.Line != null)
            {
                line.Line.dashStyle = "8.0,2.0";
                line.Line.endArrow = 6;
                line.Line.startArrow = 0;
            }
        }
    }
}
