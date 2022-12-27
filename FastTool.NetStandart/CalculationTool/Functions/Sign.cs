using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Sign : IFunction
{
    public string[] Names => new string[] { "sign" };

    public ICalculateble[] Args { get; }

    public Sign(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);

        return new Complex(Math.Sign(num.Real), 0);
    }

    public override string ToString() => $"sign({Args[0]})";
}
