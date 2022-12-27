using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Acsc : IFunction
{
    public string[] Names => new string[] { "acsc", "acosec", "arccosec", "arccsc" };

    public ICalculateble[] Args { get; }

    public Acsc(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Asin(1 / num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acsc({Args[0]})";

}
