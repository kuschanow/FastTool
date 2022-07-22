using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastTool;

public static class Calculator
{
    private static Regex simpleExpression = new Regex(@"^(\-?\d+(\.\d+)?) ?([+\-/*^]) ?(\-?\d+(\.\d+)?)$");
    private static Regex divisionExpression = new Regex(@"^(\-?\d+) ?\% ?(\-?\d+)$");

    public static double Calculate(string expression) { return Calculate(expression, 6); }
    public static double Calculate(string expression, int digits)
    {
        double result = 0;

        if (simpleExpression.IsMatch(expression))
        {
            result = SimpleCalculate(expression);
        }
        else if (divisionExpression.IsMatch(expression))
        {
            result = Convert.ToDouble(DivisionCalculate(expression));
        }

        return Math.Round(result, digits);
    }

    private static double SimpleCalculate(string expression)
    {
        Match match = simpleExpression.Match(expression);

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

    private static int DivisionCalculate(string expression)
    {
        Match match = divisionExpression.Match(expression);

        int num1 = Convert.ToInt32(match.Groups[1].Value),
            num2 = Convert.ToInt32(match.Groups[2].Value);

        return num1 % num2;
    }

}
