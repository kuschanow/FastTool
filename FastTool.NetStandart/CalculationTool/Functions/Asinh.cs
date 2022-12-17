using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Asinh : IFunction
{
    public string[] Names => new string[] { "asinh", "arsh", "arsinh" };

    public ICalculateble[] Args { get; }

    public Asinh(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Log(num + Math.Pow((num * num + 1), 0.5), Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asinh({Args[0]})";

}
