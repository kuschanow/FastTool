using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Sqrt : IFunction
{
    public string[] Names => new string[] { "sqrt" };

    public ICalculateble[] Args { get; }

    public Sqrt(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);

        return Math.Pow(num, 1 / 2);
    }

    public override string ToString() => $"sqrt({Args[0]})";

}
