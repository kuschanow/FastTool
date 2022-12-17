using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Cosh : IFunction
{
    public string[] Names => new string[] { "cosh", "ch" };

    public ICalculateble[] Args { get; }

    public Cosh(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Math.Cosh(num);
    }

    public override string ToString() => $"cosh({Args[0]})";

}
