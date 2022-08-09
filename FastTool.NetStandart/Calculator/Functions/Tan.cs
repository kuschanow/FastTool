using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Tan : IFunction
{
    private readonly object arg;

    public Tan(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return Math.Tan(num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return Math.Tan(num);
    }

    public override string ToString()
    {
        return $"tan({arg})";
    }

}
