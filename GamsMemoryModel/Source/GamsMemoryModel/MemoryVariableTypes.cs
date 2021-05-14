namespace GamsMemoryModel
{
    /// <summary>
    /// Every gams variable is one of this types.
    /// </summary>
    public enum MemoryVariableTypes
    {
        /// <summary>
        /// Unkown variable type
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// A binary variable
        /// </summary>
        Binary = 1,
        /// <summary>
        /// A integer variable
        /// </summary>
        IntegerVariable = 2,
        /// <summary>
        /// A positive variable
        /// </summary>
        Positive = 3,
        /// <summary>
        /// A negatve variable
        /// </summary>
        Negative = 4,
        /// <summary>
        /// A free variable
        /// </summary>
        Free = 5,
        /// <summary>
        /// Special order set 1
        /// </summary>
        SOS1 = 6,
        /// <summary>
        /// Special order set 2
        /// </summary>
        SOS2 = 7,
        /// <summary>
        /// A semi continous variable
        /// </summary>
        SemiCont = 8,
        /// <summary>
        /// A semi integer variable
        /// </summary>
        SemiInt = 9
    }
}