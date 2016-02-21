using EA;
using eatogliffy.gliffy.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder
{
    class StageBuilder
    {
        private GliffyStage gliffyStage;
        private Diagram eaDiagram;

        public StageBuilder()
        {
            
        }

        public StageBuilder withEaDiagram(Diagram diagram)
        {
            eaDiagram = diagram;
            return this;
        }

        public StageBuilder build()
        {
            gliffyStage = new GliffyStage();

            gliffyStage.printModel = buildPrintModel();
            gliffyStage.fitBB = buildBoundaryBox();
            gliffyStage.layers = new List<GliffyLayer>() { buildLayer() };

            return this;
        }

        public GliffyStage getStage()
        {
            return gliffyStage;
        }

        private GliffyPrintModel buildPrintModel()
        {
            GliffyPrintModel printModel = new GliffyPrintModel();
            gliffyStage.printModel.pageSize = "a4";
            gliffyStage.printModel.portrait = false;
            gliffyStage.printModel.fitToOnePage = false;
            gliffyStage.printModel.displayPageBreaks = false;

            return printModel;
        }

        private GliffyLayer buildLayer()
        {
            GliffyLayer gliffyLayer = new GliffyLayer();
            gliffyLayer.active = true;
            gliffyLayer.order = 0;
            gliffyLayer.locked = false;
            gliffyLayer.name = "Layer 0";
            gliffyLayer.visible = true;
            
            return gliffyLayer;
        }

        private GliffyBox buildBoundaryBox ()
        {
            GliffyBox gliffyBox = new GliffyBox();
            gliffyBox.min = new GliffyLocation();
            gliffyBox.max = new GliffyLocation();

            gliffyBox.min.x = 0;
            gliffyBox.min.y = 0;
            gliffyBox.max.x = eaDiagram.cx;
            gliffyBox.max.y = eaDiagram.cy;

            return gliffyBox;
        }
    }
}
