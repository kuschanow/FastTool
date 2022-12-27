using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool;

public class Number : ICalculateble
{
    public double Num { get; }

    public Number(double num) => Num = num;

    public Complex Calculate(Mode mode) => Num;

    public override string ToString() => $"{Num}";
}
