using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Atanh : IFunction
{
    public string[] Names => new string[] { "atanh", "atgh", "arth", "artanh" };

    public ICalculateble[] Args { get; }

    public Atanh(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Log(Complex.Pow(1 - num * num, 0.5) / (1 - num), Math.E);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"atanh({Args[0]})";

}
