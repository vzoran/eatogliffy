using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.io
{
    /// <summary>
    /// Model class to describe an item in diagram tree
    /// </summary>
    public class EaObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public bool IsDiagram { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Diagram Id</param>
        /// <param name="name">Diagram name</param>
        /// <param name="isDiagram">type flag. true: this is a diagram. false: this is a folder</param>
        public EaObject(string id, string name, bool isDiagram)
        {
            Id = id;
            Name = name;
            IsDiagram = isDiagram;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Diagram Id</param>
        /// <param name="name">Diagram name</param>
        /// <param name="parentId">Id of the parent folder</param>
        /// <param name="isDiagram">type flag. true: this is a diagram. false: this is a folder</param>
        public EaObject(string id, string name, string parentId, bool isDiagram)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            IsDiagram = isDiagram;
        }
    }
}
