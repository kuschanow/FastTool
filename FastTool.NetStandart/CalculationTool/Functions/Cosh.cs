using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Cosh : IFunction
{
    public string[] Names => new string[] { "cosh", "ch" };

    public ICalculateble[] Args { get; }

    public Cosh(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Complex.Cosh(num);
    }

    public override string ToString() => $"cosh({Args[0]})";

}
