using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Exp : IFunction
{
    public string[] Names => new string[] { "exp" };

    public ICalculateble[] Args { get; }

    public Exp(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);

        return Math.Pow(Math.E, num);
    }

    public override string ToString() => $"exp({Args[0]})";
}
