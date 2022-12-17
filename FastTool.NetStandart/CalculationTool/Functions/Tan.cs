using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Tan : IFunction
{
    public string[] Names => new string[] { "tan", "tg" };

    public ICalculateble[] Args { get; }

    public Tan(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Math.Tan(num);
    }

    public override string ToString() => $"tan({Args[0]})";

}
