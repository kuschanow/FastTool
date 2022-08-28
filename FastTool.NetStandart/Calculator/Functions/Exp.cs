using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Exp : IFunction
{
    public List<object> Args { get; }

    public Exp(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);

        return Math.Pow(Math.E, num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        double answer = Math.Pow(Math.E, num);

        return answer;
    }

    public override string ToString()
    {
        return $"exp({Args[0]})";
    }
}
