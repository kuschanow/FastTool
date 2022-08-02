using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Sin : IFunction
{
    private readonly object arg;

    public Sin(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return Math.Sin(num);
    }
}
