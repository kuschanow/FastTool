using FastTool.CalculationTool.Interfaces;
using FastTool.CalculationTool.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FastTool.CalculationTool;

public class ExpressionParser
{
    private static readonly Regex constExp = CreateConstRegex();
    private static readonly Regex funcExp = CreateFuncRegex();
    private static readonly Regex numExp = new(@"(?<!#)\d+(\.\d+)?(?:(?:[Ee])([+-]?\d+))?");
    private static readonly Regex оperatorExp = new(@"(?<=[-+/*^%]|^)([-+])(?:(?=[-+])[-+]+|)(?!!|^)|(?<![-+/*^%])(?:(?=!)!+|)(!)|(?<![-+/*^%]|^)(\^)|(?<![-+/*^%]|^)([/*%])(?![/*^!%]|^)|([-+])(?![/*^!%])");
    private static readonly Regex operandExp = new(@"\\([^\\#]+)#(\d+)");
    private static readonly Regex absExp = new(@"(\\left\|)|(\\right\|)");

    private List<IFunction> functions = new();
    private List<Brakets> brakets = new();
    private List<Number> numbers = new();
    private List<IConst> consts = new();
    private List<IOperator> operators = new();
    private List<KeyValuePair<string, string>> values = new();

    public ExpressionParser(List<KeyValuePair<string, string>> values)
    {
        this.values = values.Where(v => !string.IsNullOrWhiteSpace(v.Value) && !string.IsNullOrWhiteSpace(v.Key)).ToList();
        funcExp.ToString();
        if (!ValidateValues())
        {
            throw new Exception();
        }
    }
    public ExpressionParser() : this(new List<KeyValuePair<string, string>>()) { }

    private string PreProcess(string exp)
    {
        var str = exp.Replace(" ", "");
        str = str.Replace(",", "\\sep");
        str = str.Trim(Convert.ToChar("\n"));

        var matches = absExp.Matches(str);
        int level = 0, start = 0;
        foreach (Match item in matches)
        {
            if (item.Groups[1].Success)
            {
                if (level == 0)
                    start = item.Index;

                level++;
            }
            else if (item.Groups[2].Success)
            {
                level--;

                if (level == 0)
                {
                    string newStr = PreProcess($"abs({str.Substring(start + 6, item.Index - start - 6)})");
                    str = str.Replace(str.Substring(start, item.Index + item.Length - start), newStr);
                }
            }
        }

        str = str.ToLower();
        return str;
    }

    public ICalculateble Parse(string exp)
    {
        string workableStr = PreProcess(exp);
        ICalculateble result;

        GetFunctionsList(ref workableStr);
        GetBrakets(ref workableStr);
        GetNumbers(ref workableStr);
        GetConsts(ref workableStr);
        GetValues(ref workableStr);
        GetOperators(ref workableStr);

        result = GetCalculateble(workableStr);

        return result ?? throw new Exception();
    }

    private void GetFunctionsList(ref string exp)
    {
        while (funcExp.IsMatch(exp))
        {
            int start = 0, length = 0;
            functions.Add(GetFunction(exp, ref start, ref length));

            exp = exp.Replace(exp.Substring(start, length), $"\\func#{functions.Count - 1}");
        }
    }

    private IFunction GetFunction(string exp, ref int start, ref int length)
    {
        List<ICalculateble> args = new();

        Match match = funcExp.Match(exp);

        length += match.Length;

        int startIndex = match.Index + match.Length;

        if (exp[startIndex] == '(')
        {
            length++;
            int level = 1;
            int endIndex = 0;
            for (int i = startIndex + 1; i < exp.Length; i++)
            {
                if (exp[i] == '(') level++;
                else if (exp[i] == ')') level--;
                if (level == 0)
                {
                    endIndex = i;
                    break;
                }
            }

            string argsStr = exp.Substring(startIndex + 1, endIndex - startIndex - 1);

            var seps = new Regex(@"\\sep").Matches(argsStr);

            int prev = 0;

            for (int i = 0; i < argsStr.Length; i++)
            {
                if (argsStr[i] == '(') level++;
                else if (argsStr[i] == ')') level--;

                if (level == 0 && seps.Cast<Match>().SingleOrDefault(m => m.Index == i) != null)
                {
                    args.Add(Parse(argsStr.Substring(prev, i - prev)));
                    prev = i + 4;
                    i = prev - 1;
                }

                if (level == 0 && i + 1 == argsStr.Length)
                {
                    args.Add(Parse(argsStr.Substring(prev)));
                }
            }

            length += endIndex - startIndex;
        }
        else
        {
            if (constExp.Match(exp.Substring(startIndex)).Index + startIndex == startIndex)
            {
                var _const = constExp.Match(exp.Substring(startIndex));
                length += _const.Length;
                args.Add(GetConstByName(_const.Value));
            }
            else if (numExp.Match(exp.Substring(startIndex)).Index + startIndex == startIndex)
            {
                var num = numExp.Match(exp.Substring(startIndex));
                args.Add(GetNumber(num.Value, ref start, ref length));
            }
            else if (funcExp.Match(exp.Substring(startIndex)).Index == startIndex)
            {
                args.Add(GetFunction(exp.Substring(startIndex), ref start, ref length));
            }
        }

        start = match.Index;

        return GetFunctionByName(match.Value, args.ToArray());
    }

    private void GetBrakets(ref string exp)
    {
        int level = 0;
        int start = 0;

        for (int i = 0; i < exp.Length; i++)
        {
            if (exp[i] == '(')
            {
                level++;
                if (level == 1) start = i;
            }
            else if (exp[i] == ')')
            {
                level--;
                if (level == 0)
                {
                    brakets.Add(new Brakets(Parse(exp.Substring(start + 1, i - start - 1))));
                    exp = exp.Replace(exp.Substring(start, i - start + 1), $"\\br#{brakets.Count - 1}");
                }
            }
        }
    }

    private void GetNumbers(ref string exp)
    {
        while (numExp.IsMatch(exp))
        {
            int start = 0, length = 0;
            numbers.Add(GetNumber(exp, ref start, ref length));

            exp = exp.Remove(start, length);
            exp = exp.Insert(start, $"\\num#{numbers.Count - 1}");
        }
    }

    private Number GetNumber(string exp, ref int start, ref int length)
    {
        Match match = numExp.Match(exp);

        start = match.Index;
        length += match.Length;

        return new Number(Convert.ToDouble(match.Value.Replace(".", ",")));
    }

    private void GetConsts(ref string exp)
    {
        foreach (Match item in constExp.Matches(exp))
        {
            consts.Add(GetConstByName(item.Value));

            exp = exp.Replace(exp.Substring(item.Index, item.Length), $"\\const#{functions.Count - 1}");
        }
    }

    private void GetValues(ref string exp)
    {
        for (int i = 0; i < values.Count; i++)
        {
            var item = values[i];

            var regex = new Regex($@"(?<=\\(?:[^\\#]+)#(?:\d+)|[-+*/^!%]|{string.Join("|", values.Select(v => v.Key))}|^){item.Key}(?=\\(?:[^\\#]+)#(?:\d+)|[-+*/^!%]|{string.Join("|", values.Select(v => v.Key))}|$)");

            if (true)
            {

            }

            while (regex.IsMatch(exp))
            {
                exp = regex.Replace(exp, $"\\value#{i}");
            }
        }
    }

    private void GetOperators(ref string exp)
    {
        var reg = new Regex(@"\d\\");

        while (reg.IsMatch(exp))
        {
            var match = reg.Match(exp);
            exp = exp.Insert(match.Index + 1, "*");
        }

        while (оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[1].Success).Count() > 0)
        {
            var match = оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[1].Success).First();
            var op = operandExp.Match(exp.Substring(match.Index));
            string strOp = exp.Substring(match.Index, op.Index + op.Length);
            operators.Add(GetOperator(strOp));
            exp = exp.Replace(strOp, $"\\op#{operators.Count - 1}");
        }

        while (оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[2].Success).Count() > 0)
        {
            var match = оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[2].Success).First();
            var op = operandExp.Matches(exp.Substring(0, match.Index)).Cast<Match>().Last();
            string strOp = exp.Substring(op.Index, match.Index - op.Index + 1);
            operators.Add(GetOperator(strOp));
            exp = exp.Replace(strOp, $"\\op#{operators.Count - 1}");
        }

        while (оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[3].Success).Count() > 0)
        {
            var match = оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[3].Success).First();
            var op1 = operandExp.Matches(exp.Substring(0, match.Index)).Cast<Match>().Last();
            var op2 = operandExp.Match(exp.Substring(match.Index));
            string strOp = exp.Substring(op1.Index, match.Index + op2.Length - op1.Index + 1);
            operators.Add(GetOperator(strOp));
            exp = exp.Replace(strOp, $"\\op#{operators.Count - 1}");
        }

        while (оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[4].Success).Count() > 0)
        {
            var match = оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[4].Success).First();
            var op1 = operandExp.Matches(exp.Substring(0, match.Index)).Cast<Match>().Last();
            var op2 = operandExp.Match(exp.Substring(match.Index));
            string strOp = exp.Substring(op1.Index, match.Index + op2.Length - op1.Index + 1);
            operators.Add(GetOperator(strOp));
            exp = exp.Replace(strOp, $"\\op#{operators.Count - 1}");
        }

        while (оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[5].Success).Count() > 0)
        {
            var match = оperatorExp.Matches(exp).Cast<Match>().Where(m => m.Groups[5].Success).First();
            var op1 = operandExp.Matches(exp.Substring(0, match.Index)).Cast<Match>().Last();
            var op2 = operandExp.Match(exp.Substring(match.Index));
            string strOp = exp.Substring(op1.Index, match.Index + op2.Length - op1.Index + 1);
            operators.Add(GetOperator(strOp));
            exp = exp.Replace(strOp, $"\\op#{operators.Count - 1}");
        }
    }

    private IOperator GetOperator(string exp)
    {
        var match = оperatorExp.Match(exp);

        return match.Groups.Cast<Group>().ToList().IndexOf(match.Groups.Cast<Group>().Skip(1).Single(g => g.Success)) switch
        {
            1 => match.Groups[1].Value switch
            {
                "+" => new UPlus(Parse(exp.Substring(match.Index + 1))),
                "-" => new UMinus(Parse(exp.Substring(match.Index + 1))),
                _ => throw new NotImplementedException(),
            },
            2 => new Factor(Parse(exp.Substring(0, exp.Length - 1))),
            3 => new Exponent(Parse(exp.Substring(0, match.Index)), Parse(exp.Substring(match.Index + 1))),
            4 => match.Groups[4].Value switch
            {
                "*" => new Multiply(Parse(exp.Substring(0, match.Index)), Parse(exp.Substring(match.Index + 1))),
                "/" => new Division(Parse(exp.Substring(0, match.Index)), Parse(exp.Substring(match.Index + 1))),
                "%" => new Mod(Parse(exp.Substring(0, match.Index)), Parse(exp.Substring(match.Index + 1))),
                _ => throw new NotImplementedException(),
            },
            5 => match.Groups[5].Value switch
            {
                "+" => new Plus(Parse(exp.Substring(0, match.Index)), Parse(exp.Substring(match.Index + 1))),
                "-" => new Minus(Parse(exp.Substring(0, match.Index)), Parse(exp.Substring(match.Index + 1))),
                _ => throw new NotImplementedException(),
            },
            _ => throw new NotImplementedException(),
        };
    }

    private ICalculateble GetCalculateble(string exp)
    {
        var match = operandExp.Match(exp);
        int index = Convert.ToInt16(match.Groups[2].Value);

        return match.Groups[1].Value switch
        {
            "func" => functions[index],
            "br" => brakets[index],
            "num" => numbers[index],
            "const" => consts[index],
            "op" => operators[index],
            "value" => new Value(values[index].Key, Parse(values[index].Value)),
            _ => throw new NotImplementedException(),
        };
    }

    private IFunction GetFunctionByName(string func, ICalculateble[] Args)
    {
        var type = GetFunctions()
            .Select(t => new KeyValuePair<Type, string[]>(t, GetProp(t, "Names", new Type[] { typeof(ICalculateble[]) }, new object[] { new ICalculateble[0] }) as string[]))
            .Single(p => p.Value.Contains(func)).Key;
        var function = type.GetConstructor(new Type[] { typeof(ICalculateble[]) })
            .Invoke(new object[] { Args }) as IFunction;

        return function;
    }

    private IConst GetConstByName(string name)
    {
        var type = GetConsts()
            .Select(t => new KeyValuePair<Type, string[]>(t, GetProp(t, "Names", new Type[0], new object[0]) as string[]))
            .Single(p => p.Value.Contains(name)).Key;
        return type.GetConstructor(new Type[0])
            .Invoke(new object[0]) as IConst;
    }

    private static List<Type> GetFunctions()
    {
        var assembly = typeof(ExpressionParser).Assembly;
        return assembly.GetTypes().Where(t => t.Namespace == "FastTool.CalculationTool.Functions" && t.Name != "NullFunc").ToList();
    }

    private static List<Type> GetConsts()
    {
        var assembly = typeof(ExpressionParser).Assembly;
        return assembly.GetTypes().Where(t => t.Namespace == "FastTool.CalculationTool.Constants").ToList();
    }

    private static object GetProp(Type t, string name, Type[] types, object[] parametrs)
    {
        var prop = t.GetProperty(name);
        var method = prop.GetMethod;
        var ctor = t.GetConstructor(types);
        var obj = ctor.Invoke(parametrs);
        return method.Invoke(obj, new object[] { });
    }

    private static Regex CreateFuncRegex()
    {
        string regex = "(";

        var functions = GetFunctions()
            .SelectMany(t => GetProp(t, "Names", new Type[] { typeof(ICalculateble[]) }, new object[] { new ICalculateble[0] }) as string[])
            .OrderBy(s => s)
            .ToList();

        regex = @$"{string.Join("|", functions)}";

        return new Regex(regex);
    }

    private static Regex CreateConstRegex()
    {
        var consts = GetConsts()
            .SelectMany(t => GetProp(t, "Names", new Type[0], new object[0]) as string[])
            .OrderBy(s => s)
            .ToList();

        return new Regex(@$"(?<=\\(?:[^\\#]+)#(?:\d+)|[-+*/^!%]|^){string.Join("|", consts)}(?=\\(?:[^\\#]+)#(?:\d+)|[-+*/^!%]|$)");
    }

    public static List<string> GetReservedNames()
    {
        var list = new List<string>();

        var functions = GetFunctions()
            .SelectMany(t => GetProp(t, "Names", new Type[] { typeof(ICalculateble[]) }, new object[] { new ICalculateble[0] }) as string[])
            .OrderBy(s => s)
            .ToList();

        var consts = GetConsts()
            .SelectMany(t => GetProp(t, "Names", new Type[0], new object[0]) as string[])
            .OrderBy(s => s)
            .ToList();

        list.AddRange(functions);
        list.AddRange(consts);

        return list;
    }

    private bool ValidateValues()
    {
        List<KeyValuePair<string, List<string>>> addictions = values.Select(v => new KeyValuePair<string, List<string>>(v.Key, values.Where(_v =>
        {
            var regex = new Regex($@"(?<=\\(?:[^\\#]+)#(?:\d+)|[-+*/!%]|(?:{funcExp})|(?:{constExp})|^){v.Key}(?=\\(?:[^\\#]+)#(?:\d+)|[-+*/!%]|(?:{funcExp})|(?:{constExp})|$)");

            return regex.IsMatch(_v.Value);
        }).Select(__v => __v.Key).ToList())
        ).ToList();

        foreach (var item in addictions)
        {
            if (!CheckName(item.Key, addictions, new()))
                return false;
        }

        return true;
    }

    private static bool CheckName(string name, List<KeyValuePair<string, List<string>>> addictions, List<string> addictionTree)
    {
        var pare = addictions.Single(a => a.Key == name);

        if (pare.Value.Count == 0)
        {
            return true;
        }

        foreach (var item in pare.Value)
        {
            if (addictionTree.Contains(item))
            {
                return false;
            }

            addictionTree.Add(name);

            if (CheckName(item, addictions, addictionTree))
            {
                addictionTree = new();
                continue;
            }
            break;
        }

        return addictionTree.Count == 0;
    }
}
