﻿using SpiceSharp.Entities;
using SpiceSharp.Simulations;
using System;
using SpiceSharp.Components.SamplerBehaviors;
using SpiceSharp.ParameterSets;
using SpiceSharp.Behaviors;
using System.Collections.Generic;

namespace SpiceSharp.Components
{
    /// <summary>
    /// A sampler that forces a time simulation to hit certain time-points
    /// using timestep truncation.
    /// </summary>
    /// <seealso cref="IParameterized{P}"/>
    /// <seealso cref="Parameters"/>
    public class Sampler : Entity<SamplerBindingContext>,
        IParameterized<Parameters>
    {
        /// <inheritdoc/>
        public Parameters Parameters { get; } = new Parameters();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sampler"/> class.
        /// </summary>
        /// <param name="name">The name of the sampler.</param>
        public Sampler(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sampler"/> class.
        /// </summary>
        /// <param name="name">The name of the sampler.</param>
        /// <param name="timepoints">The timepoints to hit.</param>
        public Sampler(string name, IEnumerable<double> timepoints)
            : this(name)
        {
            Parameters.Points = timepoints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sampler"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="timepoints">The timepoint.</param>
        /// <param name="callback">The callback method.</param>
        public Sampler(string name, IEnumerable<double> timepoints, EventHandler<ExportDataEventArgs> callback)
            : this(name)
        {
            Parameters.Points = timepoints;
            if (callback != null)
                Parameters.ExportSimulationData += callback;
        }
    }
}
