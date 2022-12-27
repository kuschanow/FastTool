using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Coth : IFunction
{
    public string[] Names => new string[] { "coth", "cth", "ctgh" };

    public ICalculateble[] Args { get; }

    public Coth(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Complex.Tanh(num);
    }

    public override string ToString() => $"coth({Args[0]})";

}
