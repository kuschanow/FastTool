using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Log : IFunction
{
    public List<object> Args { get; }

    public Log(object firstArg, object secondArg)
    {
        Args = new List<object>() { firstArg, secondArg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num1 = calc.Transform(Args[0]);
        double num2 = calc.Transform(Args[1]);

        return Math.Log(num2, num1);
    }
    public double Calculate(Calculator calc)
    {
        double num1 = calc.Transform(Args[0]);
        double num2 = calc.Transform(Args[1]);

        return Math.Log(num2, num1);
    }

    public override string ToString()
    {
        return $"log({Args[0]})({Args[1]})";
    }

}
