using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Csc : IFunction
{
    public string[] Names => new string[] { "csc", "cosec" };

    public ICalculateble[] Args { get; }

    public Csc(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Math.Sin(num);
    }

    public override string ToString() => $"csc({Args[0]})";

}
