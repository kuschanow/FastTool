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
    private object Ans { get; set; }

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
            int indexM = exp.Exp.IndexOf(Sign.Мultiply);
            int indexD = exp.Exp.IndexOf(Sign.Division);
            int index = indexM > 0 && indexD > 0 ? Math.Min(indexM, indexD) : Math.Max(indexM, indexD);

            double num1 = Transform(exp.Exp[index - 1]);
            double num2 = Transform(exp.Exp[index + 1]);

            double result = index == indexM ? num1 * num2 : num1 / num2;

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

        answer = Math.Round(Transform(exp.Exp[0]), Digits);

        if (answer == 0)
        {
            return 0;
        }

        return answer;
    }

    public double Calculate(Expression exp, object ans)
    {
        Ans = ans;

        return Calculate(exp);
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
