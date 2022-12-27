using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Sin : IFunction
{
    public string[] Names => new string[] { "sin" };

    public ICalculateble[] Args { get; }

    public Sin(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Complex.Sin(num);
    }

    public override string ToString() => $"sin({Args[0]})";

}
