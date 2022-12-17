using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class Asec : IFunction
{
    public string[] Names => new string[] { "asec", "arcsec" };

    public ICalculateble[] Args { get; }

    public Asec(ICalculateble[] args) => Args = args;

    public double Calculate(Mode mode)
    {
        double num = Args[0].Calculate(mode);
        double answer = Math.Acos(1 / num);

        return ModeTransformator.FromRad(answer, mode);
    }

    public override string ToString() => $"asec({Args[0]})";

}
