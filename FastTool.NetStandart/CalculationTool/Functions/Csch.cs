using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Csch : IFunction
{
    public string[] Names => new string[] { "csch" };

    public ICalculateble[] Args { get; }

    public Csch(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Math.Sinh(num);
    }

    public override string ToString() => $"csch({Args[0]})";

}
