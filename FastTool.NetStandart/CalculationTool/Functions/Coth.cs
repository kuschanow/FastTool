using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Coth : IFunction
{
    public string[] Names => new string[] { "coth", "cth", "ctgh" };

    public ICalculateble[] Args { get; }

    public Coth(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Math.Tanh(num);
    }

    public override string ToString() => $"coth({Args[0]})";

}
