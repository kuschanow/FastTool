using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Sec : IFunction
{
    public string[] Names => new string[] { "sec" };

    public ICalculateble[] Args { get; }

    public Sec(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Math.Cos(num);
    }

    public override string ToString() => $"sec({Args[0]})";

}
