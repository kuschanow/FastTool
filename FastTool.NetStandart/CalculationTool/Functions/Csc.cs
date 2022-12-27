using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Csc : IFunction
{
    public string[] Names => new string[] { "csc", "cosec" };

    public ICalculateble[] Args { get; }

    public Csc(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Complex.Sin(num);
    }

    public override string ToString() => $"csc({Args[0]})";

}
