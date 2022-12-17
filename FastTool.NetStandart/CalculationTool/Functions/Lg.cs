using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Lg : IFunction
{
    public string[] Names => new string[] { "lg" };

    public ICalculateble[] Args { get; }

    public Lg(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);

        return Math.Log(num, 10);
    }

    public override string ToString() => $"log({Args[0]})";

}
