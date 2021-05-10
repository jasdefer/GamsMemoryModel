using GamsMemoryModel;
using System.Linq;

namespace GamsMemoryModelTest
{
    public class DataGenerator
    {
        public static MemoryDatabase CreateTransportDatabase()
        {
            var setI = new MemorySet("I", 1, "canning plants");
            setI.AddElement("seattle");
            setI.AddElement("san-diego");

            var setJ = new MemorySet("J", 1, "markets");
            setJ.AddElement("new-york");
            setJ.AddElement("chicago");
            setJ.AddElement("topeka");

            var parameterA = new MemoryParameter("a", 1, "capacity of plant i in cases");
            parameterA.AddRecord("seattle", 350);
            parameterA.AddRecord("san-diego", 600);

            var parameterB = new MemoryParameter("b", 1, "demand at market j in cases");
            parameterB.AddRecord("new-york", 325);
            parameterB.AddRecord("chicago", 300);
            parameterB.AddRecord("topeka", 275);

            var parameterD = new MemoryParameter("d", 2, "distance in thousands of miles");
            parameterD.AddRecord(new GamsKey("seattle", "new-york"), 2.5);
            parameterD.AddRecord(new GamsKey("seattle", "chicago"), 1.7);
            parameterD.AddRecord(new GamsKey("seattle", "topeka"), 1.8);
            parameterD.AddRecord(new GamsKey("san-diego", "new-york"), 2.5);
            parameterD.AddRecord(new GamsKey("san-diego", "chicago"), 1.8);
            parameterD.AddRecord(new GamsKey("san-diego", "topeka"), 1.4);

            var parameterF = new MemoryParameter("f", 0, "freight in dollars per case per thousand miles");
            parameterF.AddRecord(new GamsKey(), 90);

            var parameterC = new MemoryParameter("c", 2, "transport cost in thousands of dollars per case");
            foreach (var distance in parameterD.Records)
            {
                parameterC.AddRecord(distance.Key, distance.Value * parameterF.Records[new GamsKey()]);
            }

            var variableX = new MemoryVariable("x", 2, MemoryVariableTypes.Positive, "shipment quantities in cases");
            foreach (var i in setI.Elements)
            {
                foreach (var j in setJ.Elements)
                {
                    var gamsKey = new GamsKey(i.Keys.Single(), j.Keys.Single());
                    var variableValues = new VariableValues(0, 0);
                    variableX.AddRecord(gamsKey, variableValues);
                }
            }

            var variableZ = new MemoryVariable("z", 0, MemoryVariableTypes.Free, "total transportation costs in thousands of dollars");
            variableZ.AddRecord(new GamsKey(), new VariableValues());

            var sets = new MemorySet[]
            {
                setI,
                setJ
            };

            var parameters = new MemoryParameter[]
            {
                //parameterA,
                //parameterB,
                //parameterC,
                parameterD,
                //parameterF,
            };

            var variables = new MemoryVariable[]
            {
                //variableX,
                //variableZ
            };
            var memoryDatabase = new MemoryDatabase("transport", sets, parameters, variables);
            return memoryDatabase;
        }
    }
}