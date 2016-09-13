using EaToGliffy.Gliffy.Builder.Tools;
using NUnit.Framework;
using NSubstitute;
using System;
using EA;

namespace eatogliffyTest.gliffy.builder.tools
{
    [TestFixture]
    public class IdManagerUnitTest
    {
        private const string key01 = "key01";
        private const string key02 = "key02";

        [Test]
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

        [Test]
        public void TestReset()
        {
            IdManager.Reset();

            IdManager.GetId(key01);
            IdManager.GetId(key02);
            Assert.AreNotEqual(0, IdManager.Counter, "Last index of IDManager cannot be 0");

            IdManager.Reset();
            Assert.AreEqual(0, IdManager.Counter, 0f, "Last index of IDManager must be 0 after reset");
        }

        [Test]
        public void TestIdByIndex()
        {
            var mockedEaRepository = Substitute.For<Repository>();
            var mockedElement01 = Substitute.For<Element>();
            mockedElement01.ElementGUID.Returns("0001");
            var mockedElement02 = Substitute.For<Element>();
            mockedElement01.ElementGUID.Returns("0002");
            
            mockedEaRepository.GetElementByID(0).Returns(mockedElement01);
            mockedEaRepository.GetElementByID(1).Returns(mockedElement02);

            IdManager.Reset();
            IdManager.Initialize(mockedEaRepository);
            Assert.AreEqual(0, IdManager.GetIdByIndex(0), 0f, "GetIdByIndex - First index must be 0.");
            Assert.AreEqual(1, IdManager.GetIdByIndex(1), 0f, "GetIdByIndex - Second index must be 1.");
            Assert.AreEqual(0, IdManager.GetIdByIndex(0), 0f, "GetIdByIndex - First index must be 0 always.");
        }
    }
}
