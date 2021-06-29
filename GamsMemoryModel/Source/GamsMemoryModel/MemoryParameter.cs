using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamsMemoryModel
{
    /// <summary>
    /// Represents a gams parameter.
    /// Every record of this parameter has a value and a unique key (<see cref="GamsKey"/>).
    /// </summary>
    public class MemoryParameter
    {
        private Dictionary<GamsKey, double> records;

        #region JsonHelper
        /// <summary>
        /// This property is needed to convert the records to a valid json string.
        /// The keys of dictionaries are written as strings (not json formatted).
        /// This construct replaces the dictionary with a collection of key value pairs, which can be serialized without problems.
        /// </summary>
        [JsonProperty]
        private IReadOnlyCollection<KeyValuePair<GamsKey, double>> SerializedRecords
        {
            get { return records.ToList(); }
            set { records = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        #endregion

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
        [JsonIgnore]
        public IReadOnlyDictionary<GamsKey, double> Records => records;

        /// <summary>
        /// Add a new record to this parameter.
        /// </summary>
        /// <param name="gamsKey">The <see cref="GamsKey"/> uniquely identifying this record.</param>
        /// <param name="value">The value of the record.</param>
        public void AddRecord(double value, GamsKey gamsKey)
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
        public void AddRecord(double value, string key)
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
            AddRecord(value, gamsKey);
        }

        /// <summary>
        /// Add a new record to this parameter.
        /// </summary>
        /// <param name="value">The value of the record.</param>
        /// <param name="keys">The keys for this record.</param>
        public void AddRecord(double value, params string[] keys)
        {
            if (keys.Length != Dimension)
            {
                throw new ArgumentException($"Cannot add a record with a dimension of {keys.Length} to a parameter with a dimension of {Dimension}.", nameof(keys));
            }

            var gamsKey = new GamsKey(keys);
            AddRecord(value, gamsKey);
        }
    }
}