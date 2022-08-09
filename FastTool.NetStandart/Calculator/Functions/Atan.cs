using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Atan : IFunction
{
    private readonly object arg;

    public Atan(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        double answer = Math.Atan(num);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        double answer = Math.Atan(num);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"atan({arg})";
    }

}
