using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Cos : IFunction
{
    public string[] Names => new string[] { "cos" };

    public ICalculateble[] Args { get; }

    public Cos(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Math.Cos(num);
    }

    public override string ToString() => $"cos({Args[0]})";

}
