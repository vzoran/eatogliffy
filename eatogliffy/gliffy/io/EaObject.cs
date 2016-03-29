using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.io
{
    public class EaObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public bool IsDiagram { get; set; }

        public EaObject(string id, string name, bool isDiagram)
        {
            Id = id;
            Name = name;
            IsDiagram = isDiagram;
        }

        public EaObject(string id, string name, string parentId, bool isDiagram)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            IsDiagram = isDiagram;
        }
    }
}
