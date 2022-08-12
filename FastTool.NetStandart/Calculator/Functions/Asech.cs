using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asech : IFunction
{
    private readonly object arg;

    public Asech(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = Math.Log(1 + Math.Pow(1 - num * num, 0.5) / num, Math.E);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        double answer = Math.Log(1 + Math.Pow(1 - num * num, 0.5) / num, Math.E);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"asech({arg})";
    }

}
