using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Sech : IFunction
{
    public string[] Names => new string[] { "sech", "sch" };

    public ICalculateble[] Args { get; }

    public Sech(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Complex.Cosh(num);
    }

    public override string ToString() => $"sech({Args[0]})";

}
