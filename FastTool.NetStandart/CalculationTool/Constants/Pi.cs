using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Constants;

public class Pi : IConst
{
    public string[] Names => new string[] { "𝜋", "π", "pi" };

    public double Value => Math.PI;

    public Complex Calculate(Mode mode) => Value;

    public override string ToString() => $"{Names[0]}";
}
