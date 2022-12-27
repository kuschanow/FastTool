using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Asech : IFunction
{
    public string[] Names => new string[] { "asech", "arsch", "arsech" };

    public ICalculateble[] Args { get; }

    public Asech(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Log(1 + Complex.Pow(1 - num * num, 0.5) / num, Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asech({Args[0]})";

}
