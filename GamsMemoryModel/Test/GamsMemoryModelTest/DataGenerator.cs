namespace GamsMemoryModelTest;

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
        parameterA.AddRecord(350, "seattle");
        parameterA.AddRecord(600, "san-diego");

        var parameterB = new MemoryParameter("b", 1, "demand at market j in cases");
        parameterB.AddRecord(325, "new-york");
        parameterB.AddRecord(300, "chicago");
        parameterB.AddRecord(275, "topeka");

        var parameterD = new MemoryParameter("d", 2, "distance in thousands of miles");
        parameterD.AddRecord(2.5, new GamsKey("seattle", "new-york"));
        parameterD.AddRecord(1.7, new GamsKey("seattle", "chicago"));
        parameterD.AddRecord(1.8, new GamsKey("seattle", "topeka"));
        parameterD.AddRecord(2.5, new GamsKey("san-diego", "new-york"));
        parameterD.AddRecord(1.8, new GamsKey("san-diego", "chicago"));
        parameterD.AddRecord(1.4, new GamsKey("san-diego", "topeka"));

        var parameterF = new MemoryParameter("f", 0, "freight in dollars per case per thousand miles");
        parameterF.AddRecord(90, new GamsKey());

        var parameterC = new MemoryParameter("c", 2, "transport cost in thousands of dollars per case");
        foreach (var distance in parameterD.Records)
        {
            parameterC.AddRecord(distance.Value * parameterF.Records[new GamsKey()], distance.Key);
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
                parameterA,
                parameterB,
                parameterC,
                parameterD,
                parameterF,
        };

        var variables = new MemoryVariable[]
        {
                variableX,
                variableZ
        };
        var memoryDatabase = new MemoryDatabase("transport", sets, parameters, variables);
        return memoryDatabase;
    }
}
