using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Sinh : IFunction
{
    public List<object> Args { get; }

    public Sinh(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);
        num = calc.ConvertToRad(num);

        return Math.Sinh(num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        num = calc.ConvertToRad(num);

        return Math.Sinh(num);
    }

    public override string ToString()
    {
        return $"sinh({Args[0]})";
    }

}
