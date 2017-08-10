﻿using System.Collections.Generic;
using SpiceSharp.Components;
using SpiceSharp.Parser.Readers.Extensions;

namespace SpiceSharp.Parser.Readers
{
    /// <summary>
    /// A class that can read a diode model
    /// </summary>
    public class DiodeReader : ComponentReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DiodeReader() : base('d') { }

        /// <summary>
        /// Generate a diode
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="netlist">Netlist</param>
        /// <returns></returns>
        protected override CircuitComponent Generate(string name, List<object> parameters, Netlist netlist)
        {
            Diode dio = new Diode(name);
            dio.ReadNodes(parameters, 2);

            if (parameters.Count < 3)
                throw new ParseException(parameters[1], "Model expected", false);
            dio.Model = parameters[2].ReadModel<DiodeModel>(netlist);

            // Optional: Area
            if (parameters.Count > 3)
                dio.Set("area", parameters[3].ReadValue());

            // Read the rest of the parameters
            for (int i = 4; i < parameters.Count; i++)
            {
                if (parameters[i].TryReadLiteral("on"))
                    dio.Set("off", false);
                else if (parameters[i].TryReadLiteral("off"))
                    dio.Set("on", true);
                else if (parameters[i].TryReadAssignment(out string pname, out string pvalue))
                {
                    if (pname != "ic")
                        throw new ParseException(parameters[i], "IC expected");
                    dio.Set("ic", pvalue);
                }
                else if (parameters[i].TryReadValue(out pvalue))
                    dio.Set("temp", pvalue);
                else
                    throw new ParseException(parameters[i], "Unrecognized parameter");
            }

            return dio;
        }
    }
}
