using GamsMemoryModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GamsHelperTest.Model
{
    [TestClass]
    public class MemorySetTest
    {
        [TestMethod]
        public void AddDuplicate()
        {
            var set = new MemorySet("I", 1);
            set.AddElement("i1");
            set.AddElement("i1");
            Assert.AreEqual(1, set.Elements.Count);
        }

        [TestMethod]
        public void AddInvalidDimension1()
        {
            var set = new MemorySet("I", 2);
            Assert.ThrowsException<ArgumentException>(() => set.AddElement("i1"));

        }

        [TestMethod]
        public void AddInvalidDimension2()
        {
            var set = new MemorySet("I", 1);
            var gamsKey = new GamsKey("i1", "j2");
            Assert.ThrowsException<ArgumentException>(() => set.AddElement(gamsKey));
        }

        [TestMethod]
        public void AddParams()
        {
            var set = new MemorySet("IJ", 2);
            set.AddElement("i1", "j1");
            set.AddElement("i1", "j1");
            Assert.AreEqual(1, set.Elements.Count);
        }
    }
}