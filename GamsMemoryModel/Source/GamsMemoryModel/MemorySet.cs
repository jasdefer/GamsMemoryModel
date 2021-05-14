﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamsMemoryModel
{
    /// <summary>
    /// Represents a gams set.
    /// A set is a collection of elements, where each element is represented by a <see cref="GamsKey"/>.
    /// </summary>
    public class MemorySet
    {
        private HashSet<GamsKey> elements;

        #region JsonHelper
        /// <summary>
        /// This property is needed to convert the elements to a valid json string.
        /// Hashsets cannot be serialized with complex types.
        /// </summary>
        [JsonProperty]
        private IReadOnlyCollection<GamsKey> SerializedElements
        {
            get { return elements.ToArray(); }
            set { elements = new HashSet<GamsKey>(value); }
        }
        #endregion

        /// <summary>
        /// Create a new memory set.
        /// </summary>
        /// <param name="identifier">The name of the set.</param>
        /// <param name="dimension">The dimension (number of keys of each element) of this set.</param>
        /// <param name="description">The explanatory text of the set.</param>
        public MemorySet(string identifier, int dimension, string description = "")
        {
            if (dimension < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            Identifier = identifier;
            Dimension = dimension;
            Description = description;
            elements = new HashSet<GamsKey>();
        }

        /// <summary>
        /// The name of the set.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// The dimension (number of keys of each element) of this set.
        /// </summary>
        /// 
        public int Dimension { get; }
        /// <summary>
        /// The explanatory text of the set.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The collection of elements of this set.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<GamsKey> Elements => elements;

        /// <summary>
        /// Add a new element to the set.
        /// Duplicate entries are beeing ignored.
        /// </summary>
        /// <param name="gamsKey">The element to be added to this set.</param>
        public void AddElement(GamsKey gamsKey)
        {
            if (gamsKey is null)
            {
                throw new ArgumentNullException(nameof(gamsKey));
            }

            if (gamsKey.Length != Dimension)
            {
                throw new ArgumentException($"Cannot add a record with a dimension of {gamsKey.Length} to a set with a dimension of {Dimension}.", nameof(gamsKey));
            }
            elements.Add(gamsKey);
        }

        /// <summary>
        /// Add an element to this set, if it is one dimensional.
        /// Duplicate entries are beeing ignored.
        /// </summary>
        /// <param name="element">The name of the element which only has one dimension.</param>
        public void AddElement(string element)
        {
            if (Dimension != 1)
            {
                throw new ArgumentException($"Cannot add a one dimensional element to a set with a dimension of {Dimension}.");
            }
            var gamsKey = new GamsKey(element);
            AddElement(gamsKey);
        }
    }
}