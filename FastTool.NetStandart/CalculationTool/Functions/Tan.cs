using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Tan : IFunction
{
    public string[] Names => new string[] { "tan", "tg" };

    public ICalculateble[] Args { get; }

    public Tan(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Complex.Tan(num);
    }

    public override string ToString() => $"tan({Args[0]})";

}
