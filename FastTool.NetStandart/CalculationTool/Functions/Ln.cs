using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Ln : IFunction
{
    public string[] Names => new string[] { "ln" };

    public ICalculateble[] Args { get; }

    public Ln(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);

        return Math.Log(num, Math.E);
    }

    public override string ToString() => $"ln({Args[0]})";

}
