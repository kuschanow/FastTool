using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Root : IFunction
{
    private readonly object firstArg;
    private readonly object secondArg;

    public Root(object firstArg, object secondArg)
    {
        this.firstArg = firstArg;
        this.secondArg = secondArg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num1 = calc.Transform(firstArg);
        double num2 = calc.Transform(secondArg);

        return Math.Pow(num2, 1 / num1);
    }
    public double Calculate(Calculator calc)
    {
        double num1 = calc.Transform(firstArg);
        double num2 = calc.Transform(secondArg);

        return Math.Pow(num2, 1 / num1);
    }

    public override string ToString()
    {
        return $"root({firstArg})({secondArg})";
    }

}
