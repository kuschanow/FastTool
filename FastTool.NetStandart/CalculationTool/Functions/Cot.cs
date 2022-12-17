using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Cot : IFunction
{
    public string[] Names => new string[] { "cot", "ctg", "cotan" };

    public ICalculateble[] Args { get; }

    public Cot(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Math.Tan(num);
    }

    public override string ToString() => $"cot({Args[0]})";

}
