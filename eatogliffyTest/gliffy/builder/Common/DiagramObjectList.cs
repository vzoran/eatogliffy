using EA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace eatogliffyTest.gliffy.builder.Common
{
    public class DiagramObjectList : Collection
    {
        Dictionary<String, DiagramObject> innerList = new Dictionary<String, DiagramObject>();

        public short Count
        {
            get
            {
                return (short)innerList.Count;
            }
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otDiagramObject;
            }
        }

        public void AddNew(string Name, DiagramObject diagramObject)
        {
            if (!innerList.ContainsKey(Name))
            {
                innerList.Add(Name, diagramObject);
            }
        }

        public dynamic AddNew(string Name, string Type)
        {
            throw new NotImplementedException();
        }

        public void Delete(short index)
        {
            throw new NotImplementedException();
        }

        public void DeleteAt(short index, bool Refresh)
        {
            throw new NotImplementedException();
        }

        public dynamic GetAt(short index)
        {
            throw new NotImplementedException();
        }

        public dynamic GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
