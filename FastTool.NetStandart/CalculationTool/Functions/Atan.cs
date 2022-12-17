using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Atan : IFunction
{
    public string[] Names => new string[] { "atan", "atg", "arctan", "arctg" };

    public ICalculateble[] Args { get; }

    public Atan(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Atan(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"atan({Args[0]})";

}
