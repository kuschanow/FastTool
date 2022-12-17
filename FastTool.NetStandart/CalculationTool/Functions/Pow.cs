using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Pow : IFunction
{
    public int ArgsCount => 2;

    public string[] Names => new string[] { "pow" };

    public ICalculateble[] Args { get; }

    public Pow(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num1 = Args[0].Calculate(mode);
        double num2 = Args[1].Calculate(mode);

        return Math.Pow(num1, num2);
    }

    public override string ToString() => $"pow({Args[0]}, {Args[1]})";

}
