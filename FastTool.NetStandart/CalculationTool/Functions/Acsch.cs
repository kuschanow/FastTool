using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Acsch : IFunction
{
    public string[] Names => new string[] { "acsch", "acosech", "arcsch", };

    public ICalculateble[] Args { get; }

    public Acsch(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Log(1 + Math.Sign(num) * Math.Pow(1 + num * num, 0.5) / num, Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acsch({Args[0]})";

}
