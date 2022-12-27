using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Sec : IFunction
{
    public string[] Names => new string[] { "sec" };

    public ICalculateble[] Args { get; }

    public Sec(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Complex.Cos(num);
    }

    public override string ToString() => $"sec({Args[0]})";

}
