using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Tanh : IFunction
{
    private readonly object arg;

    public Tanh(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return Math.Tanh(num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return Math.Tanh(num);
    }

    public override string ToString()
    {
        return $"tanh({arg})";
    }

}
