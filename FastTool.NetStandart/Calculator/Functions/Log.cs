using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Log : IFunction
{
    private readonly object Base;
    private readonly object Arg;

    public Log(object firstArg, object secondArg)
    {
        Base = firstArg;
        Arg = secondArg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num1 = calc.Transform(Base);
        double num2 = calc.Transform(Arg);

        return Math.Log(num2, num1);
    }
}
