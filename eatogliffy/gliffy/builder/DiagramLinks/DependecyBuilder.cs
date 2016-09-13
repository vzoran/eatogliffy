using EaToGliffy.Gliffy.Model.Graphics;

namespace EaToGliffy.Gliffy.Builder.DiagramLinks
{
    public class DependecyBuilder : LinkBuilder
    {
        protected override void buildProperties()
        {
            base.buildProperties();
            this.gliffyLink.Uid = "com.gliffy.shape.uml.uml_v2.class.dependency";
        }

        protected override void buildGraphic()
        {
            base.buildGraphic();
            GliffyGraphicLine line = this.gliffyLink.Graphic as GliffyGraphicLine;

            if(line != null && line.Line != null)
            {
                line.Line.DashStyle = "8.0,2.0";
                line.Line.EndArrow = 6;
                line.Line.StartArrow = 0;
            }
        }
    }
}
