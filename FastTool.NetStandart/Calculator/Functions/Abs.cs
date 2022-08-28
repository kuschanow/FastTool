using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Abs : IFunction
{
    public List<object> Args { get; }

    public Abs(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);

        return Math.Abs(num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        double answer = Math.Abs(num);

        return answer;
    }

    public override string ToString()
    {
        return $"abs({Args[0]})";
    }
}
