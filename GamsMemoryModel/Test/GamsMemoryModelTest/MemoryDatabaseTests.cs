using GamsMemoryModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace GamsMemoryModelTest
{
    [TestClass]
    public class MemoryDatabaseTests
    {
        [TestMethod]
        public void Deserialization()
        {
            var transport = DataGenerator.CreateTransportDatabase();
            var json = transport.ToJson();
            File.WriteAllText("database.json", json);
            var transportDeserialized = MemoryDatabase.FromJson(json);
            var json2 = transportDeserialized.ToJson();

            Assert.IsNotNull(transportDeserialized);
            Assert.AreEqual(transport.Sets.Count, transportDeserialized.Sets.Count);
            Assert.AreEqual(transport.Name, transportDeserialized.Name);
            foreach (var set in transport.Sets)
            {
                var setDeserialized = transportDeserialized.Sets.Single(x => x.Identifier == set.Identifier);
                Assert.AreEqual(set.Description, setDeserialized.Description);
                Assert.AreEqual(set.Dimension, setDeserialized.Dimension);
                Assert.AreEqual(set.Elements.Count, setDeserialized.Elements.Count);
                foreach (var element in set.Elements)
                {
                    Assert.IsTrue(setDeserialized.Elements.Contains(element));
                }
            }

            Assert.AreEqual(transport.Parameters.Count, transportDeserialized.Parameters.Count);
            foreach (var parameter in transport.Parameters)
            {
                var paramterDeserialized = transportDeserialized.Parameters.Single(x => x.Identifier == parameter.Identifier);
                Assert.AreEqual(parameter.Description, paramterDeserialized.Description);
                Assert.AreEqual(parameter.Dimension, paramterDeserialized.Dimension);
                Assert.AreEqual(parameter.Records.Count, paramterDeserialized.Records.Count);
                foreach (var record in parameter.Records)
                {
                    Assert.AreEqual(record.Value, paramterDeserialized.Records[record.Key]);
                }
            }

            Assert.AreEqual(transport.Variables.Count, transportDeserialized.Variables.Count);
            foreach (var variable in transport.Variables)
            {
                var variableDeserialized = transportDeserialized.Variables.Single(x => x.Identifier == variable.Identifier);
                Assert.AreEqual(variable.Description, variableDeserialized.Description);
                Assert.AreEqual(variable.Dimension, variableDeserialized.Dimension);
                Assert.AreEqual(variable.Records.Count, variableDeserialized.Records.Count);
                Assert.AreEqual(variable.VariableType, variableDeserialized.VariableType);
                foreach (var record in variable.Records)
                {
                    Assert.AreEqual(record.Value.Level, variableDeserialized.Records[record.Key].Level);
                    Assert.AreEqual(record.Value.UpperBound, variableDeserialized.Records[record.Key].UpperBound);
                    Assert.AreEqual(record.Value.LowerBound, variableDeserialized.Records[record.Key].LowerBound);
                    Assert.AreEqual(record.Value.Scale, variableDeserialized.Records[record.Key].Scale);
                    Assert.AreEqual(record.Value.Marginal, variableDeserialized.Records[record.Key].Marginal);
                }
            }

            Assert.AreEqual(json, json2);
        }
    }
}
