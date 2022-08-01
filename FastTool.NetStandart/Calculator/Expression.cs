using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastTool;

public class Expression
{
    private static readonly Regex funcExp = new(@"((a|arc)?(sin|cos|tan|tg|ctg|cot)|(log|ln|lg|sqrt|cbrt|root|abs|pow))(\(|([+-]?\d+([.,]\d+)?))");
    private static readonly Regex numExp = new(@"(?<!\d)[+-]?\d+([.,]\d+)?");
    private static readonly Regex signExp = new(@"[+/*-]");
    private static readonly Regex absExp = new(@"\(\||\|\)|\|");


    public readonly List<object> Exp;

    public Expression(string exp)
    {
        exp = exp.Replace(" ", "");
        exp = exp.Replace('.', ',');
        exp = exp.ToLower();
        Exp = Convert(exp);
    }

    private List<object> Convert(string exp)
    {
        List<object> Exp = new List<object>();

        for (int i = 0; i < exp.Length;)
        {
            if (numExp.IsMatch(exp))
            {
                MatchCollection matches = numExp.Matches(exp);

                bool numFind = false;

                foreach (Match match in matches)
                {
                    if (match.Index == i)
                    {
                        Exp.Add(double.Parse(match.Value));
                        i += match.Value.Length;
                        numFind = true;
                        break;
                    }
                    if (match.Index > i)
                    {
                        break;
                    }
                }
                if (numFind) continue;
            }

            if (signExp.IsMatch(exp))
            {
                if ("+-*/".Contains(exp[i].ToString()))
                {
                    switch (exp[i])
                    {
                        case '+':
                            Exp.Add(Sign.Plus);
                            break;

                        case '-':
                            Exp.Add(Sign.Minus);
                            break;

                        case '*':
                            Exp.Add(Sign.Мultiply);
                            break;

                        case '/':
                            Exp.Add(Sign.Division);
                            break;
                    }
                    i++;
                    continue;
                }
            }

            if (exp[i] == '^')
            {
                Exp.Add('^');
                i++;
                continue;
            }

            if (exp[i] == '(')
            {
                Exp.Add(ParseBraket(ref i, exp));
                i++;
                continue;
            }

            if (exp[i] == '|')
            {
                MatchCollection matches = absExp.Matches(exp.Substring(i + 1));

                bool absFind = false;

                foreach (Match match in matches)
                {
                    if (match.Value == "|")
                    {
                        Exp.Add(new Abs(new Expression(exp.Substring(i + 1, match.Index))));
                        i += match.Index + 2;
                        absFind = true;
                        break;
                    }
                }
                if (absFind) continue;
            }

            if (funcExp.IsMatch(exp))
            {
                Match match = funcExp.Match(exp.Substring(i));

                if (match.Index == 0)
                {
                    Exp.Add(ParseFunction(ref i, exp, match));
                    i++;
                    continue;
                }
            }
            
            throw new Exception("Invalid expression");
        }

        for (int i = 1; i < Exp.Count; i++)
        {
            if (Exp[0] is Sign.Minus || Exp[0] is Sign.Plus)
            {
                double m = Exp[0] is Sign.Minus ? -1 : 1;
                Exp.RemoveAt(0);
                Exp.Insert(0, Sign.Мultiply);
                Exp.Insert(0, m);
            }
            if ((Exp[i] is Sign.Minus || Exp[0] is Sign.Plus) && Exp[i - 1] is Sign)
            {
                double m = Exp[i] is Sign.Minus ? -1 : 1;
                Exp.RemoveAt(i);
                Exp.Insert(i, Sign.Мultiply);
                Exp.Insert(i, m);
            }

            if ((Exp[i] is Expression || Exp[i] is IFunction || Exp[i] is double) && (Exp[i - 1] is Expression || Exp[i - 1] is IFunction || Exp[i - 1] is double))
            {
                Exp.Insert(i, Sign.Мultiply);
                continue;
            }

            if (i == Exp.Count - 1 && Exp[i] is Sign)
            {
                throw new Exception("Invalid expression");
            }
        }

        while (Exp.Contains('^'))
        {
            int index = Exp.LastIndexOf('^');

            Pow pow = new Pow(Exp[index - 1], Exp[index + 1]);
            Exp.RemoveAt(index);
            Exp.Insert(index, pow);
            Exp.RemoveAt(index - 1);
            Exp.RemoveAt(index);
        }
        
        return Exp;
    }

    private object ParseFunction(ref int startIndex, string exp, Match match)
    {
        bool isLogOrRoot = "log root pow".Contains(match.Groups[1].Value);

        if (match.Groups[5].Value == "(")
        {
            startIndex += match.Groups[5].Index;
            object arg1 = ParseBraket(ref startIndex, exp);
            object arg2 = null;
            if (isLogOrRoot)
            {
                startIndex++;
                arg2 = ParseBraket(ref startIndex, exp);
            }
            return GetFunction(match.Groups[1].Value, arg1, arg2);
        }
        else
        {
            double arg1 = double.Parse(match.Groups[5].Value);
            object arg2 = null;
            startIndex += match.Groups[1].Value.Length + match.Groups[5].Value.Length - 1;
            if (isLogOrRoot)
            {
                arg2 = ParseBraket(ref startIndex, exp);
            }
            return GetFunction(match.Groups[1].Value, arg1, arg2);
        }

        throw new Exception("Invalid expression");
    }

    private object ParseBraket(ref int startIndex, string exp)
    {
        int inLavel = 1;

        for (int i = startIndex + 1; i < exp.Length; i++)
        {
            if (exp[i] == '(')
            {
                inLavel++;
            }
            else if (exp[i] == ')')
            {
                inLavel--;
                if (inLavel == 0)
                {
                    Expression Exp = new Expression(exp.Substring(startIndex + 1, i - startIndex - 1));
                    startIndex = i;
                    return Exp;
                }
            }
        }

        throw new Exception("Invalid expression");
    }

    private IFunction GetFunction(string func, object firstArg, object secondArg)
    {
        switch (func)
        {
            case "abs":
                return new Abs(firstArg);

            case "root":
                return new Root(firstArg, secondArg);

            case "pow":
                return new Pow(firstArg, secondArg);

            case "log":
            case "lg":
            case "ln":
                return new Log(firstArg, secondArg);

            case "sin":
                return new Sin(firstArg);

            case "cos":
                return new Cos(firstArg);

            case "tan":
            case "tg":
                return new Tan(firstArg);

            case "cot":
            case "ctg":
                return new Cot(firstArg);

            case "sec":
                return new Sec(firstArg);

            case "csc":
            case "cosec":
                return new Csc(firstArg);

            case "asin":
            case "arcsin":
                return new Asin(firstArg);

            case "acos":
            case "arccos":
                return new Acos(firstArg);

            case "atan":
            case "atg":
            case "arctan":
            case "arctg":
                return new Atan(firstArg);

            case "acot":
            case "actg":
            case "arccot":
            case "arcctg":
                return new Acot(firstArg);

            case "asec":
            case "arcsec":
                return new Asec(firstArg);

            case "acsc":
            case "acosec":
            case "arccsc":
            case "arccosec":
                return new Acsc(firstArg);

            default:
                return new NullFunc();
        }
    }
}
