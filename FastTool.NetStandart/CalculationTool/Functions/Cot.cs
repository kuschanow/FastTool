using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Cot : IFunction
{
    public string[] Names => new string[] { "cot", "ctg", "cotan" };

    public ICalculateble[] Args { get; }

    public Cot(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        num = ModeTransformator.ToRad(num, mode);

        return 1 / Complex.Tan(num);
    }

    public override string ToString() => $"cot({Args[0]})";

}
