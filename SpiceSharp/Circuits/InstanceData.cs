﻿using System;

namespace SpiceSharp.Circuits
{
    /// <summary>
    /// Class for describing how a circuit can be instanced.
    /// </summary>
    public class InstanceData
    {
        /// <summary>
        /// Gets the instance name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the subcircuit.
        /// </summary>
        public Circuit Subcircuit { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Instance name.</param>
        public InstanceData(Circuit subcircuit)
        {
            Subcircuit = subcircuit ?? throw new ArgumentNullException(nameof(subcircuit));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subcircuit">The subcircuit.</param>
        /// <param name="name">The instance name.</param>
        public InstanceData(Circuit subcircuit, string name)
            : this(subcircuit)
        {
            Name = name;
        }

        /// <summary>
        /// Combine a name with the instance name to provide a unique identifier.
        /// </summary>
        /// <param name="name">The local name.</param>
        /// <returns></returns>
        public virtual string GenerateIdentifier(string name)
        {
            return Utility.Combine(Name, name);
        }
    }
}
