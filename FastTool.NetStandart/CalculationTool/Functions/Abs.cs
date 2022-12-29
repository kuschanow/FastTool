using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Abs : IFunction
{
    public string[] Names => new string[] { "abs" };

    public ICalculateble[] Args { get; }

    public Abs(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = num.Magnitude;

        return answer;
    }

    public override string ToString() => $"abs({Args[0]})";
}
