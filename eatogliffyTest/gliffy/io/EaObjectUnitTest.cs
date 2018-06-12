using System;
using eacore.io;
using NUnit.Framework;

namespace eatogliffyTest.gliffy.io
{
    [TestFixture]
    public class EaObjectUnitTest
    {
        [Test]
        public void TestConstrutors()
        {
            string id = "001";
            string name = "name01";
            string parentId = "002";
            EaObject eaObject01 = new EaObject(id, name, true);
            Assert.AreEqual(id, eaObject01.Id, String.Format("ID must be {0}", id));
            Assert.AreEqual(name, eaObject01.Name, String.Format("Name must be {0}", name));
            Assert.AreEqual(true, eaObject01.IsDiagram, String.Format("IsDIagram must be {0}", true));

            EaObject eaObject02 = new EaObject(id, name, parentId, true);
            Assert.AreEqual(id, eaObject02.Id, String.Format("ID must be {0}", id));
            Assert.AreEqual(name, eaObject02.Name, String.Format("Name must be {0}", name));
            Assert.AreEqual(true, eaObject02.IsDiagram, String.Format("IsDIagram must be {0}", true));
            Assert.AreEqual(parentId, eaObject02.ParentId, String.Format("ParentId must be {0}", parentId));
        }
    }
}
