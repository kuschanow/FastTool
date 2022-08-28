using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asin : IFunction
{
    public List<object> Args { get; }

    public Asin(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);
        double answer = Math.Asin(num);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        double answer = Math.Asin(num);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"asin({Args[0]})";
    }

}
