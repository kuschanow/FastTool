using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Sin : IFunction
{
    public string[] Names => new string[] { "sin" };

    public ICalculateble[] Args { get; }

    public Sin(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Math.Sin(num);
    }

    public override string ToString() => $"sin({Args[0]})";

}
