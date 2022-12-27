using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Factor : IFunction
{
    public string[] Names => new string[] { "factor", "factorial" };

    public ICalculateble[] Args { get; }

    public Factor(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        uint num = (uint)Args[0].Calculate(mode).Real;

        return new Complex(Factorial(num), 0);
    }

    public double Factorial(uint num)
    {
        int result = 1;

        for (int i = 1; i < num; i++)
        {
            result *= i + 1;
        }

        return result;
    }

    public override string ToString() => $"{Args[0]}!";
}
