using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastTool
{
    public class Visualisation
    {
        private readonly Expression Exp;

        public readonly List<VisualisationFrame> Vis;

        public Visualisation(Expression exp)
        {
            Exp = new Expression(exp.ToString());
            Vis = Visualise(new Expression(exp.ToString()));
        }

        public Visualisation(IFunction func)
        {
            Exp = new Expression(func.ToString());
            Vis = Visualise(new Expression(func.ToString()).Exp[0] as IFunction);
        }

        private List<VisualisationFrame> Visualise(Expression exp)
        {
            List<VisualisationFrame> vis = new List<VisualisationFrame>();

            while (exp.Exp.Contains(Sign.Мultiply) || exp.Exp.Contains(Sign.Division))
            {
                FindNextAction(Sign.Мultiply, Sign.Division, ref exp, ref vis);
            }

            while (exp.Exp.Contains(Sign.Plus) || exp.Exp.Contains(Sign.Minus))
            {
                FindNextAction(Sign.Plus, Sign.Minus, ref exp, ref vis);
            }

            if (exp.Exp.Count == 1 && exp.Exp[0] as IFunction != null)
            {
                SubAction(exp.Exp[0], 0, ref exp, ref vis, true);
            }

            return vis;
        }

        private List<VisualisationFrame> Visualise(IFunction func)
        {
            List<VisualisationFrame> vis = new List<VisualisationFrame>();

            object obj1, obj2;

            (obj1, obj2) = (func.Args[0], func.Args.Count == 2 ? func.Args[1] : null);

            if (obj1.GetType() != typeof(double))
            {
                FuncSubAction(obj1, 0, ref func, ref vis);
            }

            if (obj2 != null && obj2.GetType() != typeof(double))
            {
                FuncSubAction(obj2, 1, ref func, ref vis);
            }

            Expression currAction = new Expression($"{func}");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            var solution = new VisualisationSolution(answer, new Expression(func.ToString()));
            vis.Add(new VisualisationFrame(currAction, new Expression(func.ToString()), solution));

            return vis;
        }

        private void FindNextAction(Sign first, Sign second, ref Expression exp, ref List<VisualisationFrame> vis)
        {
            int index1 = exp.Exp.IndexOf(first);
            int index2 = exp.Exp.IndexOf(second);
            int index = index1 > 0 && index2 > 0 ? Math.Min(index1, index2) : Math.Max(index1, index2);

            object obj1 = exp.Exp[index - 1];
            object obj2 = exp.Exp[index + 1];

            if (obj1.GetType() != typeof(double))
            {
                SubAction(obj1, index - 1, ref exp, ref vis);
            }

            if (obj2.GetType() != typeof(double))
            {
                SubAction(obj2, index + 1, ref exp, ref vis);
            }

            string sign = exp.Exp[index] switch
            {
                Sign.Мultiply => "*",
                Sign.Division => "/",
                Sign.Plus => "+",
                Sign.Minus => "-",
                _ => throw new Exception("Not a Sign")
            };

            Expression currAction = new Expression($"{exp.Exp[index - 1]}{sign}{exp.Exp[index + 1]}");
            exp.Exp.RemoveRange(index - 1, 3);
            exp.Exp.Insert(index - 1, "<>");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            var solution = new VisualisationSolution(answer, new Expression(exp.ToString()));
            vis.Add(new VisualisationFrame(currAction, new Expression(exp.ToString()), solution));

            exp.Exp.Remove("<>");
            exp.Exp.Insert(index - 1, answer);
        }

        private void SubAction(object obj, int index, ref Expression exp, ref List<VisualisationFrame> vis, bool main = false)
        {
            Expression currAct = new Expression($"{obj}");

            exp.Exp.RemoveAt(index);
            exp.Exp.Insert(index, "<>");

            var cal = new Calculator();

            var ans = cal.Calculate(new Expression(currAct.ToString()));

            Visualisation detail = obj.GetType() == typeof(Expression) ? new Visualisation(obj as Expression) : new Visualisation(obj as IFunction);
            VisualisationSolution sol;

            if (detail.Vis.Count == 1)
            {
                sol = new VisualisationSolution(ans, new Expression(exp.ToString()));
            }
            else
            {
                sol = new VisualisationSolution(ans, new Expression(exp.ToString()), detail);
            }

            vis.Add(new VisualisationFrame(currAct, new Expression(exp.ToString()), sol, main));

            exp.Exp.Remove("<>");
            exp.Exp.Insert(index, ans);
        }

        private void FuncSubAction(object obj, int index, ref IFunction func, ref List<VisualisationFrame> vis)
        {
            Expression currAction = new Expression($"{obj}");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            Visualisation detail = obj.GetType() == typeof(Expression) ? new Visualisation(obj as Expression) : new Visualisation(obj as IFunction);
            VisualisationSolution solution;

            if (detail.Vis.Count == 1)
            {
                solution = new VisualisationSolution(answer, new Expression(func.ToString()));
            }
            else
            {
                solution = new VisualisationSolution(answer, new Expression(func.ToString()), detail);
            }

            vis.Add(new VisualisationFrame(currAction, new Expression(func.ToString()), solution, false));

            func.Args[index] = answer;
        }
    }
}
