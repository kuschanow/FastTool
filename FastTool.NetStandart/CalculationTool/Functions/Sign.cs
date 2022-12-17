using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Sign : IFunction
{
    public string[] Names => new string[] { "sign" };

    public ICalculateble[] Args { get; }

    public Sign(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);

        return Math.Sign(num);
    }

    public override string ToString() => $"OperatorEnum({Args[0]})";
}
