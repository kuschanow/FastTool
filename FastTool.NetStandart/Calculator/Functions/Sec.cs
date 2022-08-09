using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Sec : IFunction
{
    private readonly object arg;

    public Sec(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return 1 / Math.Cos(num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        num = calc.ConvertToRad(num);

        return 1 / Math.Cos(num);
    }

    public override string ToString()
    {
        return $"sec({arg})";
    }

}
