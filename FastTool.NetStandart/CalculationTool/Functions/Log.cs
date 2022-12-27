using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Log : IFunction
{
    public int ArgsCount => 2;

    public string[] Names => new string[] { "log" };

    public ICalculateble[] Args { get; }

    public Log(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num1 = Args[0].Calculate(mode);
        Complex num2 = Args[1].Calculate(mode);

        return Complex.Log(num2, num1.Real);
    }

    public override string ToString() => $"log({Args[0]}, {Args[1]})";

}
