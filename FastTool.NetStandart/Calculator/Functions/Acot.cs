using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Acot : IFunction
{
    private readonly object arg;

    public Acot(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = (Math.PI / 2) - Math.Atan(num);

        return calc.ConvertFromRad(answer);
    }
}
