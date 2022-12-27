using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Acosh : IFunction
{
    public string[] Names => new string[] { "arch", "arcosh", "acosh" };

    public ICalculateble[] Args { get; }

    public Acosh(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Log(num + Complex.Pow(num * num - 1, 0.5), Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acosh({Args[0]})";
}
