using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Log : IFunction
{
    public int ArgsCount => 2;

    public string[] Names => new string[] { "log" };

    public ICalculateble[] Args { get; }

    public Log(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num1 = Args[0].Calculate(mode);
        double num2 = Args[1].Calculate(mode);

        return Math.Log(num2, num1);
    }

    public override string ToString() => $"log({Args[0]}, {Args[1]})";

}
