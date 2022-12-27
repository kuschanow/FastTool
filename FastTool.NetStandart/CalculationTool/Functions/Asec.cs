using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Asec : IFunction
{
    public string[] Names => new string[] { "asec", "arcsec" };

    public ICalculateble[] Args { get; }

    public Asec(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = Complex.Acos(1 / num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asec({Args[0]})";

}
