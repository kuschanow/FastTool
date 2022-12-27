using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool;

public class Number : ICalculateble
{
    public Complex Num { get; }

    public Number(Complex num) => Num = num;

    public Complex Calculate(Mode mode) => Num;

    public override string ToString() => $"({Num.Real}, {Num.Imaginary}i)";
}
