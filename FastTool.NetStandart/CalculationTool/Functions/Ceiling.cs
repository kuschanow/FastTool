using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions
{
    public class Ceiling : IFunction
    {
        public string[] Names => new string[] { "ceiling" };

        public ICalculateble[] Args { get; }

        public Ceiling(ICalculateble[] args) => Args = args;

        public Complex Calculate(Mode mode)
        {
            Complex complex = Args[0].Calculate(mode);
            double real = Math.Ceiling(complex.Real);
            double imaginary = Math.Ceiling(complex.Imaginary);

            return new(real, imaginary);
        }

        public override string ToString() => $"ceiling({Args[0]})";
    }
}
