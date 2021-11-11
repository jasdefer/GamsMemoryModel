namespace GamsHelperTest.Model
{
    [TestClass]
    public class GamsKeyTests
    {
        [DataTestMethod]
        [DataRow(new string[] { "key1" })]
        [DataRow(new string[] { "key1, key2" })]
        [DataRow(new string[] { "key1, key2, key3" })]
        public void HashCodeMatch(string[] keys)
        {
            var gamsKey1 = new GamsKey(keys);
            var gamsKey2 = new GamsKey(keys);
            Assert.AreEqual(gamsKey1.GetHashCode(), gamsKey2.GetHashCode());
        }

        [TestMethod]
        public void HashCodeNoMatch1()
        {
            var gamsKey1 = new GamsKey("key1");
            var gamsKey2 = new GamsKey("key2");
            Assert.AreNotEqual(gamsKey1.GetHashCode(), gamsKey2.GetHashCode());
        }

        [TestMethod]
        public void HashCodeNoMatch2()
        {
            var gamsKey1 = new GamsKey("key1", "key2");
            var gamsKey2 = new GamsKey("key2", "key1");
            Assert.AreNotEqual(gamsKey1.GetHashCode(), gamsKey2.GetHashCode());
        }

        [DataTestMethod]
        [DataRow(new string[] { "key1" })]
        [DataRow(new string[] { "key1, key2" })]
        [DataRow(new string[] { "key1, key2, key3" })]
        public void AreEqual(string[] keys)
        {
            var gamsKey1 = new GamsKey(keys);
            var gamsKey2 = new GamsKey(keys);
            Assert.AreEqual(gamsKey1, gamsKey2);
        }

        [TestMethod]
        public void AreNotEqual1()
        {
            var gamsKey1 = new GamsKey("key1");
            var gamsKey2 = new GamsKey("key2");
            Assert.AreNotEqual(gamsKey1, gamsKey2);
        }

        [TestMethod]
        public void AreNotEqual2()
        {
            var gamsKey1 = new GamsKey("key1", "key2");
            var gamsKey2 = new GamsKey("key2", "key1");
            Assert.AreNotEqual(gamsKey1, gamsKey2);
        }
    }
}