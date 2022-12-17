using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Acot : IFunction
{
    public string[] Names => new string[] { "acot", "actg", "arccot", "arcctg", "acotan", "arccotan" };

    public ICalculateble[] Args { get; }

    public Acot(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = (Math.PI / 2) - Math.Atan(num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"acot({Args[0]})";

}
