using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Cbrt : IFunction
{
    public string[] Names => new string[] { "cbrt" };

    public ICalculateble[] Args { get; }

    public Cbrt(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);

        return Complex.Pow(num, 1 / 3);
    }

    public override string ToString() => $"cbrt({Args[0]})";

}
