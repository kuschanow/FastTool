using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Atan : IFunction
{
    public string[] Names => new string[] { "atan", "atg", "arctan", "arctg" };

    public ICalculateble[] Args { get; }

    public Atan(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Atan(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"atan({Args[0]})";

}
