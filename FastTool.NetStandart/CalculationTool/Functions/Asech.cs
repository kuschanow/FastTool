using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Asech : IFunction
{
    public string[] Names => new string[] { "asech", "arsch", "arsech" };

    public ICalculateble[] Args { get; }

    public Asech(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Log(1 + Math.Pow(1 - num * num, 0.5) / num, Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asech({Args[0]})";

}
