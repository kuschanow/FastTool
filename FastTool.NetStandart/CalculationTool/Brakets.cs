using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool;

public class Brakets : ICalculateble
{
    public ICalculateble Exp { get; init; }

    public Brakets(ICalculateble exp) => Exp = exp;

    public Complex Calculate(Mode mode) => Exp.Calculate(mode);

    public override string ToString() => $"({Exp})";
}
