using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Acot : IFunction
{
    public string[] Names => new string[] { "acot", "actg", "arccot", "arcctg", "acotan", "arccotan" };

    public ICalculateble[] Args { get; }

    public Acot(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode);
        Complex answer = (Math.PI / 2) - Complex.Atan(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acot({Args[0]})";

}
