using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Asin : IFunction
{
    public string[] Names => new string[] { "asin", "arcsin" };

    public ICalculateble[] Args { get; }

    public Asin(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Asin(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asin({Args[0]})";

}
