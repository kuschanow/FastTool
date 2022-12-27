using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Ln : IFunction
{
    public string[] Names => new string[] { "ln" };

    public ICalculateble[] Args { get; }

    public Ln(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);

        return Complex.Log(num);
    }

    public override string ToString() => $"ln({Args[0]})";

}
