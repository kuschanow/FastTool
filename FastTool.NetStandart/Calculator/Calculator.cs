using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FastTool;

public class Calculator
{
    public Mode Mode { get; set; }
    public int Digits { get; set; }

    public Calculator() : this(FastTool.Mode.Deg) { }
    public Calculator(Mode mode) : this(mode, 10) { }
    public Calculator(Mode mode, int digits)
    {
        Mode = mode;
        Digits = digits;
    }

    public double Calculate(Expression exp)
    {
        double answer = 0;

        while (exp.Exp.Contains(Sign.Мultiply) || exp.Exp.Contains(Sign.Division))
        {
            int indexM = exp.Exp.IndexOf(Sign.Мultiply);
            int indexD = exp.Exp.IndexOf(Sign.Division);
            int index = indexM > 0 && indexD > 0 ? Math.Min(indexM, indexD) : Math.Max(indexM, indexD);

            double num1 = Transform(exp.Exp[index - 1]);
            double num2 = Transform(exp.Exp[index + 1]);

            double result = index == indexM ? num1 * num2 : num1 / num2 ;

            exp.Exp.RemoveRange(index - 1, 3);
            exp.Exp.Insert(index - 1, result);
        }

        while (exp.Exp.Contains(Sign.Plus) || exp.Exp.Contains(Sign.Minus))
        {
            int indexP = exp.Exp.IndexOf(Sign.Plus);
            int indexM = exp.Exp.IndexOf(Sign.Minus);
            int index = indexP > 0 && indexM > 0 ? Math.Min(indexP, indexM) : Math.Max(indexP, indexM);

            double num1 = Transform(exp.Exp[index - 1]);
            double num2 = Transform(exp.Exp[index + 1]);

            double result = index == indexP ? num1 + num2 : num1 - num2;

            exp.Exp.RemoveRange(index - 1, 3);
            exp.Exp.Insert(index - 1, result);
        }

        answer = Transform(exp.Exp[0]);

        return Math.Round(answer, Digits);
    }

    public double Transform(object obj)
    {
        double answer = 0;

        if ((obj as Expression) != null)
        {
            answer = Calculate(obj as Expression);
        }
        else if ((obj as IFunction) != null)
        {
            answer = (obj as IFunction).Calculate(Mode, Digits);
        }
        else
        {
            answer = (double)(obj as double?);
        }

        return answer;
    }

    public double ConvertToRad(double num)
    {
        switch (Mode)
        {
            case Mode.Deg:
                return num *= (Math.PI / 180);

            case Mode.Rad:
                return num;

            case Mode.Grad:
                return num *= (Math.PI / 200);

            default:
                return num;
        }
    }
    public double ConvertFromRad(double num)
    {
        switch (Mode)
        {
            case Mode.Deg:
                return num /= (Math.PI / 180);

            case Mode.Rad:
                return num;

            case Mode.Grad:
                return num /= (Math.PI / 200);

            default:
                return num;
        }
    }
}
