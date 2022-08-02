using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asec : IFunction
{
    private readonly object arg;

    public Asec(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = Math.Acos(1 / num);

        return calc.ConvertFromRad(answer);
    }
}
