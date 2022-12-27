using FastTool.CalculationTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FastTool.CalculationTool.Functions
{
    public class GetComplex : IFunction
    {
        public string[] Names => new string[] { "getim" };

        public ICalculateble[] Args { get; }

        public GetComplex(ICalculateble[] args) => Args = args;

        public Complex Calculate(Mode mode) => new Complex(Args[0].Calculate(mode).Real, Args[1].Calculate(mode).Real);

        public override string ToString() => $"getim({Args[0]}, {Args[0]})";
    }
}
