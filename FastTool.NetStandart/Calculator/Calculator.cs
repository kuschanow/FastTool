using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastTool;

public static class Calculator
{
    private static Regex simpleExpression = new Regex(@"^(\-?\d+([.,]\d+)?) *([+\-/*^]) *(\-?\d+([.,]\d+)?)$");
    private static Regex oneNumExpression = new Regex(@"^(\-?\d+([.,]\d+)?) *$");
    private static Regex divisionExpression = new Regex(@"^(\-?\d+) *\% *(\-?\d+)$");
    private static Regex multiDiviDegExpression = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[*/^] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex addSubExpression = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[+\-] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex bkt = new Regex(@"[()]");

    public static double Calculate(string exp) { return Calculate(exp, 6); }
    public static double Calculate(string exp, int digits)
    {
        double result = 0;

        if (simpleExpression.IsMatch(exp))
        {
            result = SimpleCalculate(exp);
        }
        else if (divisionExpression.IsMatch(exp))
        {
            result = Convert.ToDouble(DivisionCalculate(exp));
        }
        else if (oneNumExpression.IsMatch(exp))
        {
            result = Convert.ToDouble(exp.Trim());
        }

        return Math.Round(result, digits);
    }

    private static double SimpleCalculate(string exp)
    {
        Match match = simpleExpression.Match(exp);

        double result = 0;

        double num1 = Convert.ToDouble(match.Groups[1].Value.Replace('.', ',')),
               num2 = Convert.ToDouble(match.Groups[4].Value.Replace('.', ','));

        switch (match.Groups[3].Value)
        {
            case "+":
                result = num1 + num2;
                break;

            case "-":
                result = num1 - num2;
                break;

            case "/":
                result = num1 / num2;
                break;

            case "*":
                result = num1 * num2;
                break;

            case "^":
                result = Math.Pow(num1, num2);
                break;
        }

        return result;
    }

    private static int DivisionCalculate(string exp)
    {
        Match match = divisionExpression.Match(exp);

        int num1 = Convert.ToInt32(match.Groups[1].Value),
            num2 = Convert.ToInt32(match.Groups[2].Value);

        return num1 % num2;
    }

    public static double DifficultCalculate(string exp)
    {
        double result;

        //Находим поочередно все скобки, рекурсией вычисляем их, после находим поочередно все умножения, деления и степени,
        //вычисляем их, затем то же самое делаем с прибавлением и вычитанием, пока не останеться одно число, которое и есть результат

        int _bkt, bkt_, inLavel;

        bool smthIsFind;


        do
        {
            smthIsFind = false;
            _bkt = -1;
            bkt_ = -1;
            inLavel = 0;

            if (bkt.IsMatch(exp))
            {
                for (int i = 0; i < exp.Length; i++)
                {
                    if (exp[i] == '(')
                    {
                        if (_bkt == -1)
                        {
                            _bkt = i;
                        }
                        else
                        {
                            inLavel++;
                        }
                    }
                    else if (exp[i] == ')')
                    {
                        if (inLavel == 0)
                        {
                            bkt_ = i;
                            smthIsFind = true;
                            break;
                        }
                        else if (inLavel > 0)
                        {
                            inLavel--;
                        }
                    }
                }

                if (_bkt > -1 && bkt_ > 0)
                {
                    string newExp = exp.Substring(_bkt + 1, bkt_ - _bkt - 1);
                    string res = DifficultCalculate(newExp).ToString();
                    exp = exp.Replace($"({newExp})", res);
                }

            }
        } while (smthIsFind);

        Match addSubDegMatch = addSubExpression.Match(exp);

        do
        {
            smthIsFind = false;
            Match multiDiviDegMatch = multiDiviDegExpression.Match(exp);

            if (multiDiviDegExpression.IsMatch(exp))
            {
                string newExp = multiDiviDegMatch.Value;
                string res = SimpleCalculate(newExp).ToString();
                exp = exp.Replace(newExp, res);
                smthIsFind = true;
            }

        } while (smthIsFind);


        do
        {
            smthIsFind = false;
            Match addSubMatch = addSubExpression.Match(exp);

            if (addSubExpression.IsMatch(exp))
            {
                string newExp = addSubMatch.Value;
                string res = SimpleCalculate(newExp).ToString();
                exp = exp.Replace(newExp, res);
                smthIsFind = true;
            }

        } while (smthIsFind);

        result = Convert.ToDouble(exp);

        return result;
    }

}
