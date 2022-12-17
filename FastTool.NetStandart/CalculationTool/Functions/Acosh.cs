using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Acosh : IFunction
{
    public string[] Names => new string[] { "arch", "arcosh", "acosh" };

    public ICalculateble[] Args { get; }

    public Acosh(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Log(num + Math.Pow((num * num - 1), 0.5), Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acosh({Args[0]})";
}
