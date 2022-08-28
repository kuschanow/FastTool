using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Ln : IFunction
{
    public List<object> Args { get; }

    public Ln(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);

        return Math.Log(num, Math.E);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);

        return Math.Log(num, Math.E);
    }

    public override string ToString()
    {
        return $"ln({Args[0]})";
    }

}
