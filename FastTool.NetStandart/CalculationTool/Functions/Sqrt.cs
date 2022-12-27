using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Sqrt : IFunction
{
    public string[] Names => new string[] { "sqrt" };

    public ICalculateble[] Args { get; }

    public Sqrt(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);

        return Complex.Sqrt(num);
    }

    public override string ToString() => $"sqrt({Args[0]})";

}
