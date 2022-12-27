using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Sinh : IFunction
{
    public string[] Names => new string[] { "sinh", "sh" };

    public ICalculateble[] Args { get; }

    public Sinh(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return Complex.Sinh(num);
    }

    public override string ToString() => $"sinh({Args[0]})";

}
