using System;
using System.Collections.Generic;

namespace GamsMemoryModel
{
    /// <summary>
    /// Represents a gams variable.
    /// Every record of this variable has certain values (<see cref="VariableValues" />) and a unique key (<see cref="GamsKey"/>).
    /// </summary>
    public class MemoryVariable
    {
        private readonly Dictionary<GamsKey, VariableValues> records;


        /// <summary>
        /// Create a new <see cref="MemoryVariable"/>.
        /// </summary>
        /// <param name="identifier">The name of the variable.</param>
        /// <param name="dimension">The dimension (number of keys of each element) of this variable.</param>
        /// <param name="variableType">The type (integer, free, positive, etc.) of this variable.</param>
        /// <param name="description">The explanatory text of this variable.</param>
        public MemoryVariable(string identifier, int dimension, MemoryVariableTypes variableType, string description = "")
        {
            if (dimension < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            Identifier = identifier;
            Dimension = dimension;
            VariableType = variableType;
            Description = description;
            records = new Dictionary<GamsKey, VariableValues>();
        }

        /// <summary>
        /// The name of the variable.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// The dimension (number of keys of each element) of this variable.
        /// </summary>
        public int Dimension { get; }

        /// <summary>
        /// The type (integer, free, positive, etc.) of this variable.
        /// </summary>
        public MemoryVariableTypes VariableType { get; }

        /// <summary>
        /// The explanatory text of this variable.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The collection of records identifiable by the <see cref="GamsKey"/>.
        /// </summary>
        public IReadOnlyDictionary<GamsKey, VariableValues> Records => records;

        /// <summary>
        /// Add a new record to this variable.
        /// </summary>
        /// <param name="gamsKey">The <see cref="GamsKey"/> uniquely identifying this record.</param>
        /// <param name="value">The values (<see cref="VariableValues"/>) for this record.</param>
        public void AddRecord(GamsKey gamsKey, VariableValues value)
        {
            if (gamsKey is null)
            {
                throw new ArgumentNullException(nameof(gamsKey));
            }

            if (gamsKey.Length != Dimension)
            {
                throw new ArgumentException($"Cannot add a record with a dimension of {gamsKey.Length} to a variable with a dimension of {Dimension}.", nameof(gamsKey));
            }
            records.Add(gamsKey, value);
        }

        /// <summary>
        /// Add a new record to this variable if it is one dimensional.
        /// </summary>
        /// <param name="key">The key uniquely identifying this record.</param>
        /// <param name="level">The level of the record.</param>
        /// <param name="lowerBound">The lower bound of the record.</param>
        /// <param name="upperBound">The upper bound of the record.</param>
        public void AddRecord(string key, double level, double lowerBound, double upperBound)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Dimension != 1)
            {
                throw new ArgumentException($"Cannot add a one dimensional record to a variable with a dimension of {Dimension}.");
            }

            var gamsKey = new GamsKey(key);
            var values = new VariableValues(level, lowerBound, upperBound);
            AddRecord(gamsKey, values);
        }
    }
}