using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastTool;

public static class Calculator
{
    public enum Mode
    {
        Deg,
        Rad,
        Grad
    }

    private static Regex simpleExp = new Regex(@"^(\-?\d+([.,]\d+)?) *([+\-/*^]) *(\-?\d+([.,]\d+)?)$");
    private static Regex oneNumExp = new Regex(@"^(\-?\d+([.,]\d+)?) *$");
    private static Regex numInBktExp = new Regex(@"\((\-?\d+([.,]\d+)?)\)");
    private static Regex divisionExp = new Regex(@"^(\-?\d+) *\% *(\-?\d+)$");
    private static Regex DegExp = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[\^] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex multiDivExp = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[*/] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex addSubExp = new Regex(@"(?:(\-?\d+([.,]\d+)?)) *[+\-] *(?:(\-?\d+([.,]\d+)?))(?!\.)");
    private static Regex bktExp = new Regex(@"[()]");
    private static Regex _bktExp = new Regex(@"[(]");
    private static Regex bkt_Exp = new Regex(@"[)]");
    private static Regex numExp = new Regex(@"(?<!\d|[)]|\d |\) )\-?\d+([.,]\d+)?");
    private static Regex multiBktExp = new Regex(@"((?<!\d|[)]|\d |\) )\-?\d+([.,]\d+)?) *\(");
    private static Regex pctExp = new Regex(@"[,.]");
    private static Regex actExp = new Regex(@"[+\-/*^]");
    private static Regex othExp = new Regex(@"[^+\-/*^]");
    private static Regex sqrtExp = new Regex(@"sqrt\((.+)\)");
    private static Regex cbrtExp = new Regex(@"cbrt\((.+)\)");
    private static Regex fulltrigonometryExp = new Regex(@"^(a|arc)?(sin|cos|tan|tg|ctg|cot)\(? *(\-?\d+([.,]\d+)?) *\)?$");
    private static Regex trigonometryExp = new Regex(@"(a|arc)?(sin|cos|tan|tg|ctg|cot)\(? *(\-?\d+([.,]\d+)?) *\)?");
    private static Regex funcExp = new Regex(@"(a|arc)?(sin|cos|tan|tg|ctg|cot)");
    private static Regex fullabsExp = new Regex(@"^\| *(\-?\d+([.,]\d+)?) *\|$");
    private static Regex absExp = new Regex(@"\| *(\-?\d+([.,]\d+)?) *\|");
    private static Regex minusExp = new Regex(@"\-([^\d ])");

    public static double Calculate(string exp) { return Calculate(exp, Mode.Deg,  10); }
    public static double Calculate(string exp, Mode mode) { return Calculate(exp, mode, 10); }
    public static double Calculate(string exp, Mode mode, int digits)
    {
        double result;

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
        else if (fulltrigonometryExp.IsMatch(exp))
        {
            result = TrigonometryCalculate(exp, mode);
        }
        else if (fullabsExp.IsMatch(exp))
        {
            result = AbsCalculate(exp);
        }
        else
        {
            string tempExp = exp;

            if (minusExp.IsMatch(tempExp))
            {
                MatchCollection minusMatches = minusExp.Matches(tempExp);

                foreach (Match match in minusMatches)
                {
                    tempExp = tempExp.Replace(match.Value, $"-1 * {match.Groups[1].Value}");
                }
            }

            if (absExp.IsMatch(tempExp))
            {
                MatchCollection absMatches = absExp.Matches(tempExp);

                foreach (Match match in absMatches)
                {
                    tempExp = tempExp.Replace(match.Value, match.Groups[1].Value);
                }
            }

            if (multiBktExp.IsMatch(tempExp))
            {
                MatchCollection multiBktMatches = multiBktExp.Matches(tempExp);

                foreach (Match match in multiBktMatches)
                {
                    tempExp = tempExp.Replace(match.Value, $"{match.Groups[1].Value} * (");
                }
            }

            if (sqrtExp.IsMatch(tempExp))
            {
                MatchCollection multiBktMatches = sqrtExp.Matches(tempExp);

                foreach (Match match in multiBktMatches)
                {
                    tempExp = tempExp.Replace(match.Value, $"({match.Groups[1].Value}) ^ 0.5");
                }
            }

            if (cbrtExp.IsMatch(tempExp))
            {
                MatchCollection multiBktMatches = cbrtExp.Matches(tempExp);

                foreach (Match match in multiBktMatches)
                {
                    tempExp = tempExp.Replace(match.Value, $"({match.Groups[1].Value}) ^ (1 / 3)");
                }
            }

            tempExp = tempExp.Replace(" ", "");

            int numMathesCount = numExp.Matches(tempExp).Count;
            tempExp = numExp.Replace(tempExp, "");
            tempExp = funcExp.Replace(tempExp, "");
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
            result = DifficultCalculate(exp, mode);
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

    private static double TrigonometryCalculate(string exp, Mode mode)
    {
        Match match = trigonometryExp.Match(exp);

        double result = 0;

        double num = Convert.ToDouble(match.Groups[3].Value.Replace('.', ','));

        if (!match.Groups[1].Success)
        {
            switch (mode)
            {
                case Mode.Deg:
                    num *= (Math.PI / 180);
                    break;

                case Mode.Rad:
                    break;

                case Mode.Grad:
                    num *= (Math.PI / 200);
                    break;
            }
        }

        switch (match.Groups[2].Value)
        {
            case "sin":
                if (match.Groups[1].Success)
                {
                    result = Math.Asin(num);
                    break;
                }
                result = Math.Sin(num);
                break;

            case "cos":
                if (match.Groups[1].Success)
                {
                    result = Math.Acos(num);
                    break;
                }
                result = Math.Cos(num); break;

            case "tg":
            case "tan":
                if (match.Groups[1].Success)
                {
                    result = Math.Atan(num);
                    break;
                }
                result = Math.Tan(num); break;

            case "ctg":
            case "cot":
                if (match.Groups[1].Success)
                {
                    result = (Math.PI / 2) - Math.Atan(num);
                    break;
                }
                result = 1 / Math.Tan(num); break;
        }

        if (match.Groups[1].Success)
        {
            switch (mode)
            {
                case Mode.Deg:
                    result /= (Math.PI / 180);
                    break;

                case Mode.Rad:
                    break;

                case Mode.Grad:
                    result /= (Math.PI / 200);
                    break;
            }
        }

        return Math.Round(result, 15);
    }

    private static int DivisionCalculate(string exp)
    {
        Match match = divisionExp.Match(exp);

        int num1 = Convert.ToInt32(match.Groups[1].Value),
            num2 = Convert.ToInt32(match.Groups[2].Value);

        return num1 % num2;
    }

    private static double AbsCalculate(string exp)
    {
        Match match = absExp.Match(exp);

        double num = Convert.ToInt32(match.Groups[1].Value);

        return Math.Abs(num);
    }

    private static double DifficultCalculate(string exp, Mode mode)
    {
        double result;

        //Находим поочередно все скобки, рекурсией вычисляем их, после находим поочередно все степени, умножения и деления,
        //вычисляем их, затем то же самое делаем с прибавлением и вычитанием, пока не останеться одно число, которое и есть результат

        int _bkt, bkt_, inLavel;

        bool smthIsFind;

        if (minusExp.IsMatch(exp))
        {
            MatchCollection minusMatches = minusExp.Matches(exp);

            foreach (Match match in minusMatches)
            {
                exp = exp.Replace(match.Value, $"-1 * {match.Groups[1].Value}");
            }
        }

        do
        {
            smthIsFind = false;
            Match absMatch = absExp.Match(exp);

            if (absExp.IsMatch(exp))
            {
                string newExp = absMatch.Value;
                string res = AbsCalculate(newExp).ToString();
                exp = exp.Replace(newExp, res);
                smthIsFind = true;
            }

        } while (smthIsFind);

        if (multiBktExp.IsMatch(exp))
        {
            MatchCollection multiBktMatches = multiBktExp.Matches(exp);

            foreach (Match match in multiBktMatches)
            {
                exp = exp.Replace(match.Value, $"{match.Groups[1].Value} * (");
            }
        }

        if (sqrtExp.IsMatch(exp))
        {
            MatchCollection multiBktMatches = sqrtExp.Matches(exp);

            foreach (Match match in multiBktMatches)
            {
                exp = exp.Replace(match.Value, $"({match.Groups[1].Value}) ^ 0.5");
            }
        }

        if (cbrtExp.IsMatch(exp))
        {
            MatchCollection multiBktMatches = cbrtExp.Matches(exp);

            foreach (Match match in multiBktMatches)
            {
                exp = exp.Replace(match.Value, $"({match.Groups[1].Value}) ^ (1 / 3)");
            }
        }

        do
        {
            do
            {
                smthIsFind = false;
                Match TrigonometryMatch = trigonometryExp.Match(exp);

                if (trigonometryExp.IsMatch(exp))
                {
                    string newExp = TrigonometryMatch.Value;
                    string res = TrigonometryCalculate(newExp, mode).ToString();
                    exp = exp.Replace(newExp, res);
                    smthIsFind = true;
                }

            } while (smthIsFind);

            if (numInBktExp.IsMatch(exp))
            {
                MatchCollection numInBktMatches = numInBktExp.Matches(exp);

                foreach (Match match in numInBktMatches)
                {
                    exp = exp.Replace(match.Value, match.Groups[1].Value);
                }
            }

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
                    string res = DifficultCalculate(newExp, mode).ToString();
                    exp = exp.Replace(newExp, res);
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
