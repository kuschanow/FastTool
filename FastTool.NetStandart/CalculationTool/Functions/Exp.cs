using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Exp : IFunction
{
    public string[] Names => new string[] { "exp" };

    public ICalculateble[] Args { get; }

    public Exp(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);

        return Complex.Exp(num);
    }

    public override string ToString() => $"exp({Args[0]})";
}
