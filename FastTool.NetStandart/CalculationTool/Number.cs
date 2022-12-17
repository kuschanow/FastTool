using FastTool.CalculationTool.Interfaces;

namespace FastTool.CalculationTool;

public class Number : ICalculateble
{
    public double Num { get; }

    public Number(double num) => Num = num;

    public double Calculate(Mode mode) => Num;

    public override string ToString() => $"{Num}";
}
