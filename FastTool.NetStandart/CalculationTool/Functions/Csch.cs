using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Csch : IFunction
{
    public string[] Names => new string[] { "csch" };

    public ICalculateble[] Args { get; }

    public Csch(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Complex.Sinh(num);
    }

    public override string ToString() => $"csch({Args[0]})";

}
