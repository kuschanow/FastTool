using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Acos : IFunction
{
    public string[] Names => new string[] { "acos", "arccos" };

    public ICalculateble[] Args { get; }

    public Acos(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Acos(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acos({Args[0]})";
}
