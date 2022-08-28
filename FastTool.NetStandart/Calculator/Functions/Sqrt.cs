using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Sqrt : IFunction
{
    public List<object> Args { get; }

    public Sqrt(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);

        return Math.Pow(num, 1 / 2);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);

        return Math.Pow(num, 1 / 2);
    }

    public override string ToString()
    {
        return $"sqrt({Args[0]})";
    }

}
