namespace GamsMemoryModel
{
    /// <summary>
    /// This class stores all values of a gams variable.
    /// </summary>
    public class VariableValues
    {
        /// <summary>
        /// Create new values for a gams variable.
        /// </summary>
        /// <param name="level">The level bound of a variable.</param>
        /// <param name="lowerBound">The lower bound of a variable.</param>
        /// <param name="upperBound">The upper bound of a variable.</param>
        /// <param name="marginal">The marginal of a variable.</param>
        /// <param name="scale">The scale of a variable.</param>
        public VariableValues(double level = 0,
            double lowerBound = double.NegativeInfinity,
            double upperBound = double.PositiveInfinity,
            double marginal = 0,
            double scale = 1)
        {
            Level = level;
            Marginal = marginal;
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Scale = scale;
        }

        /// <summary>
        /// The level of a variable.
        /// </summary>
        public double Level { get; }

        /// <summary>
        /// The lower bound of a variable.
        /// </summary>
        public double LowerBound { get; }

        /// <summary>
        /// The upper bound of a variable.
        /// </summary>
        public double UpperBound { get; }

        /// <summary>
        /// The marginal of a variable.
        /// </summary>
        public double Marginal { get; }

        /// <summary>
        /// The scale of a variable.
        /// </summary>
        public double Scale { get; }
    }
}