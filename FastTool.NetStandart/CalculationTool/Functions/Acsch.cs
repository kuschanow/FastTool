using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Acsch : IFunction
{
    public string[] Names => new string[] { "acsch", "acosech", "arcsch", };

    public ICalculateble[] Args { get; }

    public Acsch(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Log(1 + Math.Sign(num.Real) * Complex.Pow(1 + num * num, 0.5) / num, Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acsch({Args[0]})";

}
