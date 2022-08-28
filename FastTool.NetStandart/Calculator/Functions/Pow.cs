using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Pow : IFunction
{
    public List<object> Args { get; }

    public Pow(object firstArg, object secondArg)
    {
        Args = new List<object>() { firstArg, secondArg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num1 = calc.Transform(Args[0]);
        double num2 = calc.Transform(Args[1]);

        return Math.Pow(num1, num2);
    }
    public double Calculate(Calculator calc)
    {
        double num1 = calc.Transform(Args[0]);
        double num2 = calc.Transform(Args[1]);

        return Math.Pow(num1, num2);
    }

    public override string ToString()
    {
        return $"pow({Args[0]})({Args[1]})";
    }

}
