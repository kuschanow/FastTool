using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Atanh : IFunction
{
    public List<object> Args { get; }

    public Atanh(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);
        double answer = Math.Log(Math.Pow(1 - num * num, 0.5)/(1 - num), Math.E);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        double answer = Math.Log(Math.Pow(1 - num * num, 0.5) / (1 - num), Math.E);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"atanh({Args[0]})";
    }

}
