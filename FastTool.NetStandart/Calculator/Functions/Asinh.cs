using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asinh : IFunction
{
    public List<object> Args { get; }

    public Asinh(object arg)
    {
        Args = new List<object>() { arg };
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(Args[0]);
        double answer = Math.Log(num + Math.Pow((num * num + 1), 0.5), Math.E);

        return calc.ConvertFromRad(answer);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(Args[0]);
        double answer = Math.Log(num + Math.Pow((num * num + 1), 0.5), Math.E);

        return calc.ConvertFromRad(answer);
    }

    public override string ToString()
    {
        return $"asinh({Args[0]})";
    }

}
