using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Visualisation(Expression exp, List<VisualisationFrame> vis)
        {
            Exp = new Expression(exp.ToString());
            Vis = vis;
        }

        private List<VisualisationFrame> Visualise(Expression exp)
        {
            List<VisualisationFrame> vis = new List<VisualisationFrame>();

            while (exp.Exp.Count > 1)
            {

                if (ActionRule(Sign.Мultiply, Sign.Division, ref exp, ref vis))
                {
                    continue;
                }

                if (ActionRule(Sign.Plus, Sign.Minus, ref exp, ref vis))
                {
                    continue;
                }
            }

            if (exp.Exp.Count == 1)
            {
                if (exp.Exp[0] as IFunction != null)
                {
                    FuncRule(exp.Exp[0] as IFunction, ref exp, ref vis);
                }
                else
                {
                    ElementRule(exp.Exp[0], 0, ref exp, ref vis);
                }
            }

            return vis;
        }

        private bool FuncRule(IFunction func, ref Expression exp, ref List<VisualisationFrame> vis)
        {
            if (exp.Exp.Select(e => (e as IFunction) != null).Count() == 0)
            {
                return false;
            }

            List<VisualisationFrame> _vis = new List<VisualisationFrame>();
            IFunction _func = new Expression(func.ToString()).Exp[0] as IFunction;

            object obj1, obj2;

            (obj1, obj2) = (func.Args[0], func.Args.Count == 2 ? func.Args[1] : null);

            FuncArgRule(obj1, 0, ref _func, ref _vis);
            FuncArgRule(obj2, 1, ref _func, ref _vis);

            Expression currAction = new Expression($"{_func}");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            var solution = new VisualisationSolution(answer, new Expression(_func.ToString()));
            _vis.Add(new VisualisationFrame(currAction, new Expression(_func.ToString()), solution));

            int i = exp.Exp.IndexOf(func);
            exp.Exp.RemoveAt(i);
            exp.Exp.Insert(i, answer);

            vis.Add(new VisualisationFrame(currAction, new Expression(func.ToString()), new VisualisationSolution(answer, new Expression(func.ToString()), new Visualisation(new Expression(func.ToString()), _vis))));

            return true;
        }

        private bool ActionRule(Sign first, Sign second, ref Expression exp, ref List<VisualisationFrame> vis)
        {
            int index1 = exp.Exp.IndexOf(first);
            int index2 = exp.Exp.IndexOf(second);
            int index = index1 > 0 && index2 > 0 ? Math.Min(index1, index2) : Math.Max(index1, index2);

            if (index < 0)
            {
                return false;
            }

            object obj1 = exp.Exp[index - 1];
            object obj2 = exp.Exp[index + 1];

            ElementRule(obj1, index - 1, ref exp, ref vis);
            ElementRule(obj2, index + 1, ref exp, ref vis);

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

            return true;
        }

        private bool ElementRule(object obj, int index, ref Expression exp, ref List<VisualisationFrame> vis)
        {
            if (obj.GetType() == typeof(double))
            {
                return false;
            }

            Expression currAct = new Expression($"{obj}");

            exp.Exp.RemoveAt(index);
            exp.Exp.Insert(index, "<>");

            var cal = new Calculator();

            var ans = cal.Calculate(new Expression(currAct.ToString()));

            Visualisation detail = new Visualisation(new Expression(obj.ToString()));
            VisualisationSolution sol;

            if (detail.Vis.Count == 1)
            {
                sol = new VisualisationSolution(ans, new Expression(exp.ToString()));
            }
            else
            {
                sol = new VisualisationSolution(ans, new Expression(exp.ToString()), detail);
            }

            vis.Add(new VisualisationFrame(currAct, new Expression(exp.ToString()), sol, (obj as Expression) == null));

            exp.Exp.Remove("<>");
            exp.Exp.Insert(index, ans);

            return true;
        }

        private bool FuncArgRule(object obj, int index, ref IFunction func, ref List<VisualisationFrame> vis)
        {
            if (obj.GetType() == typeof(double) || (obj as Expression != null && (obj as Expression).Exp.Count == 1 && (obj as Expression).Exp[0].GetType() == typeof(double)))
            {
                return false;
            }

            Expression currAction = new Expression($"{obj}");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            Visualisation detail = new Visualisation(new Expression(obj.ToString()));
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

            return true;
        }
    }
}
