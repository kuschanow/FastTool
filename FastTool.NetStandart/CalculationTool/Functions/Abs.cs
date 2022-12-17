using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Abs : IFunction
{
    public string[] Names => new string[] { "abs" };

    public ICalculateble[] Args { get; }

    public Abs(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Abs(num);

        return answer;
    }

    public override string ToString() => $"\\left|{Args[0]}\\right|";
}
