﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Exp : IFunction
{
    private readonly object arg;

    public Exp(object arg)
    {
        this.arg = arg;
    }

    public double Calculate(Mode mode, int digits)
    {
        Calculator calc = new Calculator(mode, digits);

        double num = calc.Transform(arg);

        return Math.Pow(Math.E, num);
    }
    public double Calculate(Calculator calc)
    {
        double num = calc.Transform(arg);
        double answer = Math.Pow(Math.E, num);

        return answer;
    }

    public override string ToString()
    {
        return $"exp({arg})";
    }
}