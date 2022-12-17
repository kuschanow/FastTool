using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Acoth : IFunction
{
    public string[] Names => new string[] { "acoth", "actgh", "arcth", "arcoth" };

    public ICalculateble[] Args { get; }

    public Acoth(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Log(Math.Pow(num * num - 1, 0.5) / (num - 1), Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acoth({Args[0]})";

}
