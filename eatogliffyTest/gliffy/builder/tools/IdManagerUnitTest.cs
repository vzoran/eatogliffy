using eatogliffy.gliffy.builder.tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace eatogliffyTest.gliffy.builder.tools
{
    [TestClass]
    public class IdManagerUnitTest
    {
        private const string key01 = "key01";
        private const string key02 = "key02";

        [TestMethod]
        public void TestGetIdWithKey()
        {
            IdManager.Reset();

            int key01id = IdManager.GetId(key01);
            int key02id = IdManager.GetId(key02);

            Assert.AreEqual(0, key01id, 0f, "First index must be 0");
            Assert.AreEqual(1, key02id, 0f, "Second index must be diffrerent than 0");
            Assert.AreEqual(0, key01id, 0f, String.Format("index of {0} must be 0", key01id));
            Assert.AreEqual(1, key02id, 0f, "Second index must be diffrerent than 0");
        }

        [TestMethod]
        public void TestReset()
        {
            IdManager.Reset();

            IdManager.GetId(key01);
            IdManager.GetId(key02);
            Assert.AreNotEqual(0, IdManager.Counter, 0f, "Last index of IDManager cannot be 0");

            IdManager.Reset();
            Assert.AreEqual(0, IdManager.Counter, 0f, "Last index of IDManager must be 0 after reset");
        }
    }
}
