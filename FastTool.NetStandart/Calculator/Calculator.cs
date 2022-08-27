using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FastTool;

public class Calculator : ICalculator
{
    public Mode Mode { get; set; }
    public int Digits { get; set; }
    public List<Expression> CalculationList { get; private set; }
    public Calculator() : this(Mode.Deg) { }
    public Calculator(Mode mode) : this(mode, 4) { }
    public Calculator(Mode mode, int digits)
    {
        Mode = mode;
        Digits = digits;
    }

    public double Calculate(Expression exp)
    {
        double answer;

        while (exp.Exp.Contains(Sign.Мultiply) || exp.Exp.Contains(Sign.Division))
        {
            FindNextAction(Sign.Мultiply, Sign.Division, ref exp);
        }

        while (exp.Exp.Contains(Sign.Plus) || exp.Exp.Contains(Sign.Minus))
        {
            FindNextAction(Sign.Plus, Sign.Minus, ref exp);
        }

        answer = Math.Round(Transform(exp.Exp[0]), Digits);

        if (answer == 0)
        {
            return 0;
        }

        return answer;
    }

    private void FindNextAction(Sign first, Sign second, ref Expression exp)
    {
        int index1 = exp.Exp.IndexOf(first);
        int index2 = exp.Exp.IndexOf(second);
        int index = index1 > 0 && index2 > 0 ? Math.Min(index1, index2) : Math.Max(index1, index2);

        double num1 = Transform(exp.Exp[index - 1]);
        double num2 = Transform(exp.Exp[index + 1]);

        double result = exp.Exp[index] switch
        {
            Sign.Мultiply => num1 * num2,
            Sign.Division => num1 / num1,
            Sign.Plus => num1 + num2,
            Sign.Minus => num1 - num2,
            _ => throw new Exception("Not a Sign")
        };

        exp.Exp.RemoveRange(index - 1, 3);
        exp.Exp.Insert(index - 1, result);
    }

    public double Transform(object obj)
    {
        double answer;

        if ((obj as Expression) != null)
        {
            answer = Calculate(obj as Expression);
        }
        else if ((obj as IFunction) != null)
        {
            answer = (obj as IFunction).Calculate(this);
        }
        else if ((obj as string) != null && (obj as string) == "ans")
        {
            answer = Transform(Ans);
        }
        else
        {
            try
            {
                answer = (double)obj;
            }
            catch
            {
                answer = (int)obj;
            }
        }

        return answer;
    }

    public double ConvertToRad(double num)
    {
        return Mode switch
        {
            Mode.Deg => num *= (Math.PI / 180),
            Mode.Rad => num,
            Mode.Grad => num *= (Math.PI / 200),
            _ => num,
        };
    }
    public double ConvertFromRad(double num)
    {
        return Mode switch
        {
            Mode.Deg => num /= (Math.PI / 180),
            Mode.Rad => num,
            Mode.Grad => num /= (Math.PI / 200),
            _ => num,
        };
    }
}
