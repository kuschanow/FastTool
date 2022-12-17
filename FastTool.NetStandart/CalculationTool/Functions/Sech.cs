using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Sech : IFunction
{
    public string[] Names => new string[] { "sech", "sch" };

    public ICalculateble[] Args { get; }

    public Sech(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Math.Cosh(num);
    }

    public override string ToString() => $"sech({Args[0]})";

}
