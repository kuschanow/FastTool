using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Cos : IFunction
{
    public string[] Names => new string[] { "cos" };

    public ICalculateble[] Args { get; }

    public Cos(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Complex.Cos(num);
    }

    public override string ToString() => $"cos({Args[0]})";

}
