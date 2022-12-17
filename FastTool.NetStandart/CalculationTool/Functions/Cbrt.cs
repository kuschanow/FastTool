using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Cbrt : IFunction
{
    public string[] Names => new string[] { "cbrt" };

    public ICalculateble[] Args { get; }

    public Cbrt(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);

        return Math.Pow(num, 1 / 3);
    }

    public override string ToString() => $"cbrt({Args[0]})";

}
