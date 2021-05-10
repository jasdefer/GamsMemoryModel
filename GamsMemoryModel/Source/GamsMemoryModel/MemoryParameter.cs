using System;
using System.Collections.Generic;

namespace GamsMemoryModel
{
    /// <summary>
    /// Represents a gams parameter.
    /// Every record of this parameter has a value and a unique key (<see cref="GamsKey"/>).
    /// </summary>
    public class MemoryParameter
    {
        private readonly Dictionary<GamsKey, double> records;

        /// <summary>
        /// Create a new <see cref="MemoryParameter"/>.
        /// </summary>
        /// <param name="identifier">The name of the parameter.</param>
        /// <param name="dimension">The dimension (number of keys of each element) of this parameter.</param>
        /// <param name="description">The explanatory text of this variable.</param>
        public MemoryParameter(string identifier, int dimension, string description = "")
        {
            if (dimension < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            Identifier = identifier;
            Dimension = dimension;
            Description = description;
            records = new Dictionary<GamsKey, double>();
        }

        /// <summary>
        /// The name of the parameter.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// The dimension (number of keys of each element) of this parameter.
        /// </summary>
        public int Dimension { get; }

        /// <summary>
        /// The explanatory text of this variable.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The collection of records identifiable by the <see cref="GamsKey"/>.
        /// </summary>
        public IReadOnlyDictionary<GamsKey, double> Records => records;

        /// <summary>
        /// Add a new record to this parameter.
        /// </summary>
        /// <param name="gamsKey">The <see cref="GamsKey"/> uniquely identifying this record.</param>
        /// <param name="value">The value of the record.</param>
        public void AddRecord(GamsKey gamsKey, double value = 0)
        {
            if (gamsKey is null)
            {
                throw new ArgumentNullException(nameof(gamsKey));
            }

            if (gamsKey.Length != Dimension)
            {
                throw new ArgumentException($"Cannot add a record with a dimension of {gamsKey.Length} to a parameter with a dimension of {Dimension}.", nameof(gamsKey));
            }
            records.Add(gamsKey, value);
        }

        /// <summary>
        /// Add a new record to this parameter, if it is one dimensional.
        /// </summary>
        /// <param name="key">The key of this record.</param>
        /// <param name="value">The value of this record.</param>
        public void AddRecord(string key, double value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Dimension != 1)
            {
                throw new ArgumentException($"Cannot add a one dimensional record to a parameter with a dimension of {Dimension}.");
            }

            var gamsKey = new GamsKey(key);
            AddRecord(gamsKey, value);
        }
    }
}