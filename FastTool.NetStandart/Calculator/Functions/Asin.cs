using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asin : IFunction
{
    private readonly object arg;

    public Asin(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = Math.Asin(num);

        return calc.ConvertFromRad(answer);
    }
}
