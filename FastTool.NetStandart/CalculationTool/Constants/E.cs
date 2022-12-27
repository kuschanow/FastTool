using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Constants;

public class E : IConst
{
    public string[] Names => new string[] { "e" };

    public double Value => Math.E;

    public Complex Calculate(Mode mode) => Value;

    public override string ToString() => $"{Names[0]}";
}
