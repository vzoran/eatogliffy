using eatogliffy.gliffy.builder.tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace eatogliffyTest.gliffy.builder.tools
{
    [TestClass]
    public class IdManagerUnitTest
    {
        [TestMethod]
        public void TestGetIdWithKey()
        {
            const string key01 = "key01";
            const string key02 = "key02";
            IdManager.Reset();

            int key01id = IdManager.GetId(key01);
            int key02id = IdManager.GetId(key02);

            Assert.AreEqual(0, key01id, 0f, "First index must be 0");
            Assert.AreEqual(1, key02id, 0f, "Second index must be diffrerent than 0");
            Assert.AreEqual(0, key01id, 0f, String.Format("index of {0} must be 0", key01id));
        }
    }
}
