using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Acoth : IFunction
{
    public List<object> Args { get; }

    public Acoth(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);
        double answer = Math.Log(Math.Pow(num * num - 1, 0.5) / (num - 1), Math.E);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        double answer = Math.Log(Math.Pow(num * num - 1, 0.5) / (num - 1), Math.E);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"acoth({Args[0]})";
    }

}
