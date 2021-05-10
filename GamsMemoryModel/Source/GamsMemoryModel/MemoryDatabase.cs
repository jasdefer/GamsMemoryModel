﻿using System;
using System.Collections.Generic;

namespace GamsMemoryModel
{
    /// <summary>
    /// Represents a GAMSDatabase.
    /// </summary>
    public class MemoryDatabase
    {
        /// <summary>
        /// Create a new <see cref="MemoryDatabase"/>.
        /// </summary>
        /// <param name="name">The name of this database.</param>
        /// <param name="sets">The sets in this database.</param>
        /// <param name="parameters">The parameters in this database.</param>
        /// <param name="variables">The variables in this database.</param>
        public MemoryDatabase(string name,
            IReadOnlyCollection<MemorySet> sets = null,
            IReadOnlyCollection<MemoryParameter> parameters = null,
            IReadOnlyCollection<MemoryVariable> variables = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Parameters = parameters ?? Array.Empty<MemoryParameter>();
            Sets = sets ?? Array.Empty<MemorySet>();
            Variables = variables ?? Array.Empty<MemoryVariable>();
        }

        /// <summary>
        /// The name of this database.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The sets in this database.
        /// </summary>
        public IReadOnlyCollection<MemorySet> Sets { get; }

        /// <summary>
        /// The parameters in this database.
        /// </summary>
        public IReadOnlyCollection<MemoryParameter> Parameters { get; }

        /// <summary>
        /// The variables in this database.
        /// </summary>
        public IReadOnlyCollection<MemoryVariable> Variables { get; }
    }
}