﻿using System.Collections.Generic;
using SpiceSharp.Components;
using SpiceSharp.Parser.Readers.Extensions;

namespace SpiceSharp.Parser.Readers
{
    /// <summary>
    /// This class can read voltage switches
    /// </summary>
    public class VoltageSwitchReader : ComponentReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VoltageSwitchReader() : base('s') { }

        /// <summary>
        /// Generate
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="netlist">Netlist</param>
        /// <returns></returns>
        protected override CircuitComponent Generate(string name, List<object> parameters, Netlist netlist)
        {
            VoltageSwitch vsw = new VoltageSwitch(name);
            vsw.ReadNodes(parameters, 4);

            // Read the model
            if (parameters.Count < 5)
                throw new ParseException(parameters[3], "Model expected", false);
            vsw.Model = parameters[4].ReadModel<VoltageSwitchModel>(netlist);

            // Optional ON or OFF
            if (parameters.Count == 6)
            {
                string state = parameters[5].ReadWord();
                switch (state)
                {
                    case "on": vsw.SetOn(); break;
                    case "off": vsw.SetOff(); break;
                    default: throw new ParseException(parameters[5], "ON or OFF expected");
                }
            }
            return vsw;
        }
    }
}
