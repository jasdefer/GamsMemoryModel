using GamsMemoryModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

        [TestMethod]
        public void CreateByEnumeration1()
        {
            var set = MemorySet.CreateByEnumeration("i", 3);
            Assert.IsNotNull(set);
            Assert.AreEqual(3, set.Elements.Count);
            Assert.AreEqual("i1", set.Elements.ElementAt(0).Keys.Single());
            Assert.AreEqual("i2", set.Elements.ElementAt(1).Keys.Single());
            Assert.AreEqual("i3", set.Elements.ElementAt(2).Keys.Single());
        }

        [TestMethod]
        public void CreateByEnumeration2()
        {
            var set = MemorySet.CreateByEnumeration("i", 20);
            Assert.IsNotNull(set);
            Assert.AreEqual(20, set.Elements.Count);
            Assert.AreEqual("i01", set.Elements.ElementAt(0).Keys.Single());
            Assert.AreEqual("i02", set.Elements.ElementAt(1).Keys.Single());
            Assert.AreEqual("i03", set.Elements.ElementAt(2).Keys.Single());
        }

        [TestMethod]
        public void CreateByEnumeration3()
        {
            var set = MemorySet.CreateByEnumeration("i", 20000);
            Assert.IsNotNull(set);
            Assert.AreEqual(20000, set.Elements.Count);
            Assert.AreEqual("i00001", set.Elements.ElementAt(0).Keys.Single());
            Assert.AreEqual("i00002", set.Elements.ElementAt(1).Keys.Single());
            Assert.AreEqual("i00003", set.Elements.ElementAt(2).Keys.Single());
        }
    }
}