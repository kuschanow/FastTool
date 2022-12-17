using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Asin : IFunction
{
    public string[] Names => new string[] { "asin", "arcsin" };

    public ICalculateble[] Args { get; }

    public Asin(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Asin(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asin({Args[0]})";

}
