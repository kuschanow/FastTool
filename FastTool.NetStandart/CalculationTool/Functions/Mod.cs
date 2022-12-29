using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Mod : IFunction
{
    public string[] Names => new string[] { "mod" };

    public ICalculateble[] Args { get; }

    public Mod(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex param = Args[1].Calculate(mode);
        Complex answer = new(num.Real % param.Real, num.Imaginary % param.Imaginary);

        return answer;
    }

    public override string ToString() => $"mod({Args[0]})";
}
