using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Acoth : IFunction
{
    private readonly object arg;

    public Acoth(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = Math.Log(Math.Pow(num * num - 1, 0.5) / (num - 1), Math.E);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        double answer = Math.Log(Math.Pow(num * num - 1, 0.5) / (num - 1), Math.E);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"acoth({arg})";
    }

}
