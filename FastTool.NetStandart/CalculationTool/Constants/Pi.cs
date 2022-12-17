using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Constants;

public class Pi : IConst
{
    public string[] Names => new string[] { "π", "pi" };

    public double Value => Math.PI;

    public double Calculate(Mode mode) => Value;

    public override string ToString() => $"{Names[0]}";
}
