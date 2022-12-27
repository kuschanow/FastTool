using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Asinh : IFunction
{
    public string[] Names => new string[] { "asinh", "arsh", "arsinh" };

    public ICalculateble[] Args { get; }

    public Asinh(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Log(num + Complex.Pow(num * num + 1, 0.5), Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asinh({Args[0]})";

}
