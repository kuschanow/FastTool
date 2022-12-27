using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions
{
    public class Floor : IFunction
    {
        public string[] Names => new string[] { "floor" };

        public ICalculateble[] Args { get; }

        public Floor(ICalculateble[] args) => Args = args;

        public Complex Calculate(Mode mode)
        {
            Complex complex = Args[0].Calculate(mode);
            double real = Math.Floor(complex.Real);
            double imaginary = Math.Floor(complex.Imaginary);

            return new(real, imaginary);
        }

        public override string ToString() => $"floor({Args[0]})";
    }
}
