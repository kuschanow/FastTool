using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Lg : IFunction
{
    public string[] Names => new string[] { "lg" };

    public ICalculateble[] Args { get; }

    public Lg(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);

        return Complex.Log10(num);
    }

    public override string ToString() => $"log({Args[0]})";

}
