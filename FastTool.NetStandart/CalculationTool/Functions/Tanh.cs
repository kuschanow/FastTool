using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Tanh : IFunction
{
    public string[] Names => new string[] { "tanh", "tgh", "th" };

    public ICalculateble[] Args { get; }

    public Tanh(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Math.Tanh(num);
    }

    public override string ToString() => $"tanh({Args[0]})";

}
