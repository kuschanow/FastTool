using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Acos : IFunction
{
    public string[] Names => new string[] { "acos", "arccos" };

    public ICalculateble[] Args { get; }

    public Acos(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Acos(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acos({Args[0]})";
}
