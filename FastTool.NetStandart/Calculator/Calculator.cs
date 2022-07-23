using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastTool;

public static class Calculator
{
    private static Regex simpleExp = new Regex(@"^(\-?\d+([.,]\d+)?) *([+\-/*^]) *(\-?\d+([.,]\d+)?)$");
    private static Regex oneNumExp = new Regex(@"^(\-?\d+([.,]\d+)?) *$");
    private static Regex divisionExp = new Regex(@"^(\-?\d+) *\% *(\-?\d+)$");
    private static Regex DegExp = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[\^] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex multiDivExp = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[*/] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex addSubExp = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[+\-] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex bktExp = new Regex(@"[()]");
    private static Regex _bktExp = new Regex(@"[(]");
    private static Regex bkt_Exp = new Regex(@"[)]");
    private static Regex numExp = new Regex(@"(?<!\d|[)]|\d |\) )\-?\d+([.,]\d+)?");
    private static Regex pctExp = new Regex(@"[,.]");
    private static Regex actExp = new Regex(@"[+\-/*^]");
    private static Regex othExp = new Regex(@"[^+\-/*^]");

    public static double Calculate(string exp) { return Calculate(exp, 6); }
    public static double Calculate(string exp, int digits)
    {
        double result = 0;

        if (simpleExp.IsMatch(exp))
        {
            result = SimpleCalculate(exp);
        }
        else if (divisionExp.IsMatch(exp))
        {
            result = Convert.ToDouble(DivisionCalculate(exp));
        }
        else if (oneNumExp.IsMatch(exp))
        {
            result = Convert.ToDouble(exp.Trim());
        }
        else
        {
            string tempExp = exp;

            tempExp = tempExp.Replace(" ", "");
            int numMathesCount = numExp.Matches(tempExp).Count;
            tempExp = numExp.Replace(tempExp, "");
            if (pctExp.IsMatch(tempExp))
            {
                throw new Exception("Invalid expression");
            }
            if (_bktExp.IsMatch(tempExp) || bkt_Exp.IsMatch(tempExp))
            {
                if (_bktExp.Matches(tempExp).Count != bkt_Exp.Matches(tempExp).Count)
                {
                    throw new Exception("Invalid expression");
                }
                tempExp = bktExp.Replace(tempExp, "");
            }
            if (othExp.IsMatch(tempExp))
            {
                throw new Exception("Invalid expression");
            }
            if (actExp.Matches(tempExp).Count != numMathesCount - 1)
            {
                throw new Exception("Invalid expression");
            }
            result = DifficultCalculate(exp);
        }

        return Math.Round(result, digits);
    }

    private static double SimpleCalculate(string exp)
    {
        Match match = simpleExp.Match(exp);

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
        Match match = divisionExp.Match(exp);

        int num1 = Convert.ToInt32(match.Groups[1].Value),
            num2 = Convert.ToInt32(match.Groups[2].Value);

        return num1 % num2;
    }

    private static double DifficultCalculate(string exp)
    {
        double result;

        //Находим поочередно все скобки, рекурсией вычисляем их, после находим поочередно все степени, умножения и деления,
        //вычисляем их, затем то же самое делаем с прибавлением и вычитанием, пока не останеться одно число, которое и есть результат

        int _bkt, bkt_, inLavel;

        bool smthIsFind;

        do
        {
            smthIsFind = false;
            _bkt = -1;
            bkt_ = -1;
            inLavel = 0;

            if (bktExp.IsMatch(exp))
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

        do
        {
            smthIsFind = false;
            Match DegMatch = DegExp.Match(exp);

            if (DegExp.IsMatch(exp))
            {
                string newExp = DegMatch.Value;
                string res = SimpleCalculate(newExp).ToString();
                exp = exp.Replace(newExp, res);
                smthIsFind = true;
            }

        } while (smthIsFind);

        do
        {
            smthIsFind = false;
            Match multiDiviDegMatch = multiDivExp.Match(exp);

            if (multiDivExp.IsMatch(exp))
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
            Match addSubMatch = addSubExp.Match(exp);

            if (addSubExp.IsMatch(exp))
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
