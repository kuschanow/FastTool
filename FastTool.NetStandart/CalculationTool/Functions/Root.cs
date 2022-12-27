using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Root : IFunction
{
    public int ArgsCount => 2;

    public string[] Names => new string[] { "root" };

    public ICalculateble[] Args { get; }

    public Root(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num1 = Args[0].Calculate(mode);
        Complex num2 = Args[1].Calculate(mode);

        return Complex.Pow(num2, 1 / num1);
    }

    public override string ToString() => $"root({Args[0]}, {Args[1]})";

}
