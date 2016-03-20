using EA;
using eatogliffy.gliffy.builder.diagramobject;
using eatogliffy.gliffy.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder
{
    class StageBuilder
    {
        private GliffyStage gliffyStage;
        private Diagram eaDiagram;
        private Repository eaRepository;

        private const string ORIENTATION_PORTRAIT = "P";
        private const int GUID_LENGTH = 12;
        private const int MAX_HEIGHT = 5000;
        private const int MAX_WIDTH = 5000;
        private const string DEFAULT_BACKGROUND = "#FFFFFF";

        public StageBuilder()
        {
            
        }

        public StageBuilder withEaDiagram(Diagram diagram)
        {
            eaDiagram = diagram;
            return this;
        }

        public StageBuilder withEaRepository(Repository repository)
        {
            eaRepository = repository;
            return this;
        }

        public StageBuilder build()
        {
            gliffyStage = new GliffyStage();

            buildProperties();
            buildPrintModel();
            buildBoundaryBox();
            buildLayers();
            buildObjects();
            finalizeBuild();

            return this;
        }

        public GliffyStage getStage()
        {
            return gliffyStage;
        }

        private void buildObjects()
        {
            gliffyStage.objects = new List<GliffyObject>();

            IEnumerator objectEnumerator = eaDiagram.DiagramObjects.GetEnumerator();
            while(objectEnumerator.MoveNext())
            {
                DiagramObject diagramObject = (DiagramObject)objectEnumerator.Current;
                Element currentElement = eaRepository.GetElementByID(diagramObject.ElementID);
                ObjectBuilder objectBuilder = getBuilder(currentElement.Type);

                if(objectBuilder != null)
                {
                    gliffyStage.objects.Add(
                    objectBuilder
                        .withEaObject(diagramObject)
                        .withEaElement(currentElement)
                        .withLayer(gliffyStage.layers[0].guid)
                        .buildAsParent()
                        .getObject());
                }
            }
        }

        private void finalizeBuild()
        {
            gliffyStage.nodeIndex = IdManager.Counter;
            gliffyStage.layers[0].nodeIndex = gliffyStage.objects.Count;
        }

        private ObjectBuilder getBuilder(string eaElementType)
        {
            return new RectangleBuilder();
        }

        private void buildProperties()
        {
            gliffyStage.autoFit = true;
            gliffyStage.background = DEFAULT_BACKGROUND;
            gliffyStage.drawingGuidesOn = true;
            gliffyStage.exportBorder = false;
            gliffyStage.gridOn = true;
            gliffyStage.height = eaDiagram.cy;
            gliffyStage.maxHeight = MAX_HEIGHT;
            gliffyStage.maxWidth = MAX_WIDTH;
            gliffyStage.pageBreaksOn = false;
            gliffyStage.printGridOn = false;
            gliffyStage.printPaper = null;
            gliffyStage.printPortrait = eaDiagram.Orientation.Equals(ORIENTATION_PORTRAIT);
            gliffyStage.printShrinkToFit = false;
            gliffyStage.snapToGrid = true;
            gliffyStage.themeData = null;
            gliffyStage.viewportType = "default";
            gliffyStage.width = eaDiagram.cx;
        }

        private void buildPrintModel()
        {
            GliffyPrintModel printModel = new GliffyPrintModel();
            printModel.pageSize = "a4";
            printModel.portrait = false;
            printModel.fitToOnePage = false;
            printModel.displayPageBreaks = false;

            gliffyStage.printModel = printModel;
        }

        private void buildLayers()
        {
            GliffyLayer gliffyLayer = new GliffyLayer();
            gliffyLayer.active = true;
            gliffyLayer.order = 0;
            gliffyLayer.locked = false;
            gliffyLayer.name = "Layer 0";
            gliffyLayer.visible = true;
            gliffyLayer.guid = randomString(GUID_LENGTH);

            gliffyStage.layers = new List<GliffyLayer>() { gliffyLayer };
        }

        private void buildBoundaryBox ()
        {
            GliffyBox gliffyBox = new GliffyBox();
            gliffyBox.min = new GliffyLocation();
            gliffyBox.max = new GliffyLocation();

            gliffyBox.min.x = 0;
            gliffyBox.min.y = 0;
            gliffyBox.max.x = eaDiagram.cx;
            gliffyBox.max.y = eaDiagram.cy;

            gliffyStage.fitBB = gliffyBox;
        }

        private string randomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
