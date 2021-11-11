using System;

namespace GamsMemoryModelTest;

[TestClass]
public class MemoryParameterTest
{
    [TestMethod]
    public void AddDuplicate()
    {
        var parameter = new MemoryParameter("a", 1);
        parameter.AddRecord(1, "i1");
        Assert.ThrowsException<ArgumentException>(() => parameter.AddRecord(1, "i1"));
    }

    [TestMethod]
    public void AddInvalidDimension1()
    {
        var parameter = new MemoryParameter("a", 2);
        Assert.ThrowsException<ArgumentException>(() => parameter.AddRecord(1, "i1"));
    }

    [TestMethod]
    public void AddInvalidDimension2()
    {
        var parameter = new MemoryParameter("a", 1);
        var gamsKey = new GamsKey("i1", "j2");
        Assert.ThrowsException<ArgumentException>(() => parameter.AddRecord(1, gamsKey));
    }

    [TestMethod]
    public void AddParamsDimension0()
    {
        var parameter = new MemoryParameter("a", 0);
        parameter.AddRecord(3);
        Assert.AreEqual(3, parameter.Records.Single().Value);
    }

    [TestMethod]
    public void AddParamsDimension2()
    {
        var parameter = new MemoryParameter("IJ", 2);
        parameter.AddRecord(1, "i1", "j1");
        Assert.AreEqual(1, parameter.Records.Count);
        Assert.AreEqual(1, parameter.Records.Single().Value);
    }
}
