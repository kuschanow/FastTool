using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Atanh : IFunction
{
    private readonly object arg;

    public Atanh(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = Math.Log(Math.Pow(1 - num * num, 0.5)/(1 - num), Math.E);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        double answer = Math.Log(Math.Pow(1 - num * num, 0.5) / (1 - num), Math.E);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"atanh({arg})";
    }

}
