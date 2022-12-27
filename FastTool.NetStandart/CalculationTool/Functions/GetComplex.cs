using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions
{
    public class GetComplex : IFunction
    {
        public string[] Names => new string[] { "getcom" };

        public ICalculateble[] Args { get; }

        public GetComplex(ICalculateble[] args) => Args = args;

        public Complex Calculate(Mode mode) => new(Args[0].Calculate(mode).Real, Args[1].Calculate(mode).Real);

        public override string ToString() => $"getcom({Args[0]}, {Args[1]})";
    }
}
