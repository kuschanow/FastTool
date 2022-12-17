using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Sinh : IFunction
{
    public string[] Names => new string[] { "sinh", "sh" };

    public ICalculateble[] Args { get; }

    public Sinh(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Math.Sinh(num);
    }

    public override string ToString() => $"sinh({Args[0]})";

}
