using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Constants;

public class E : IConst
{
    public string[] Names => new string[] { "e" };

    public double Value => Math.E;

    public double Calculate(Mode mode) => Value;

    public override string ToString() => $"{Names[0]}";
}
