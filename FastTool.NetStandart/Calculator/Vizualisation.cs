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

        public readonly List<IVisualisationFrame> Vis;

        public Visualisation(Expression exp)
        {
            Exp = new Expression(exp.ToString());
            Vis = Visualise(new Expression(exp.ToString()));
        }

        public Visualisation(Expression exp, List<IVisualisationFrame> vis)
        {
            Exp = new Expression(exp.ToString());
            Vis = vis;
        }

        private List<IVisualisationFrame> Visualise(Expression exp)
        {
            List<IVisualisationFrame> vis = new List<IVisualisationFrame>();

            while (exp.Exp.Count > 1)
            {
                if (RemoveZeroRule(exp, vis))
                {
                    exp = new Expression(exp.ToString());
                    continue;
                }

                if (LikeTermsRule(exp, vis))
                {
                    exp = new Expression(exp.ToString());
                    continue;
                }

                if (ActionRule(Sign.Мultiply, Sign.Division, exp, vis))
                {
                    continue;
                }

                if (ActionRule(Sign.Plus, Sign.Minus, exp, vis))
                {
                    continue;
                }
            }

            if (exp.Exp.Count == 1)
            {
                if (exp.Exp[0] as IFunction != null)
                {
                    FuncRule(exp.Exp[0] as IFunction, exp, vis);
                }
                else
                {
                    ElementRule(exp.Exp[0], 0, exp, vis);
                }
            }

            return vis;
        }

        private bool ActionRule(Sign first, Sign second, Expression exp, List<IVisualisationFrame> vis)
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

            ElementRule(obj1, index - 1, exp, vis);
            ElementRule(obj2, index + 1, exp, vis);

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

            var solution = new VisualisationSolutionCalc(answer, new Expression(exp.ToString()));
            string description = "";
            vis.Add(new VisualisationFrameCalc(currAction, new Expression(exp.ToString()), solution, description));

            exp.Exp.Remove("<>");
            exp.Exp.Insert(index - 1, answer);

            return true;
        }

        private bool ElementRule(object obj, int index, Expression exp, List<IVisualisationFrame> vis)
        {
            if (obj.GetType() == typeof(double))
            {
                return false;
            }

            Expression currAction = new Expression($"{obj}");

            exp.Exp.RemoveAt(index);
            exp.Exp.Insert(index, "<>");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            Visualisation detail = new Visualisation(new Expression(obj.ToString()));
            VisualisationSolutionCalc solution;
            if (detail.Exp.Exp[0] is IFunction && detail.Exp.Exp.Count == 1)
            {
                solution = new VisualisationSolutionCalc(answer, new Expression(exp.ToString()), detail.Vis[0].Solution.Detail);
            }
            else if (detail.Vis.Count == 1)
            {
                solution = new VisualisationSolutionCalc(answer, new Expression(exp.ToString()));
            }
            else
            {
                solution = new VisualisationSolutionCalc(answer, new Expression(exp.ToString()), detail);
            }

            string description = "";
            vis.Add(new VisualisationFrameCalc(currAction, new Expression(exp.ToString()), solution, description, (obj as Expression) == null));

            exp.Exp.Remove("<>");
            exp.Exp.Insert(index, answer);

            return true;
        }

        private bool FuncRule(IFunction func, Expression exp, List<IVisualisationFrame> vis)
        {
            if (exp.Exp.Select(e => (e as IFunction) != null).Count() == 0)
            {
                return false;
            }

            List<IVisualisationFrame> _vis = new List<IVisualisationFrame>();
            IFunction _func = new Expression(func.ToString()).Exp[0] as IFunction;

            object obj1, obj2;

            (obj1, obj2) = (func.Args[0], func.Args.Count == 2 ? func.Args[1] : null);

            FuncArgRule(obj1, 0, _func, _vis);
            FuncArgRule(obj2, 1, _func, _vis);

            Expression currAction = new Expression($"{_func}");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            string description = "";
            var solution = new VisualisationSolutionCalc(answer, new Expression(_func.ToString()));
            _vis.Add(new VisualisationFrameCalc(currAction, new Expression(_func.ToString()), solution, description));

            int i = exp.Exp.IndexOf(func);
            exp.Exp.RemoveAt(i);
            exp.Exp.Insert(i, answer);

            vis.Add(new VisualisationFrameCalc(currAction, new Expression(func.ToString()), new VisualisationSolutionCalc(answer, new Expression(func.ToString()), new Visualisation(new Expression(func.ToString()), _vis)), description));

            return true;
        }

        private bool FuncArgRule(object obj, int index, IFunction func, List<IVisualisationFrame> vis)
        {
            if (obj.GetType() == typeof(double) || (obj as Expression != null && (obj as Expression).Exp.Count == 1 && (obj as Expression).Exp[0].GetType() == typeof(double)))
            {
                return false;
            }

            Expression currAction = new Expression($"{obj}");

            var calc = new Calculator();

            var answer = calc.Calculate(new Expression(currAction.ToString()));

            Visualisation detail = new Visualisation(new Expression(obj.ToString()));
            VisualisationSolutionCalc solution;

            if (detail.Vis.Count == 1)
            {
                solution = new VisualisationSolutionCalc(answer, new Expression(func.ToString()));
            }
            else
            {
                solution = new VisualisationSolutionCalc(answer, new Expression(func.ToString()), detail);
            }

            string description = "";
            vis.Add(new VisualisationFrameCalc(currAction, new Expression(func.ToString()), solution, description, false));

            func.Args[index] = answer;

            return true;
        }

        private bool LikeTermsRule(Expression exp, List<IVisualisationFrame> vis)
        {
            var _exp = new Expression(exp.ToString());

            var indexes = exp.Exp.Select((e, c) =>
                {
                    if (e is Sign)
                    {
                        return new List<int>();
                    }

                    Sign? s = null;

                    if (c == 0)
                    {
                        if (exp.Exp[c] is double)
                        {
                            s = (double)exp.Exp[c] > 0 ? Sign.Plus : Sign.Minus;
                        }
                        else
                        {
                            s = Sign.Plus;
                        }
                    }
                    else
                    {
                        if (exp.Exp[c] is double)
                        {
                            s = exp.Exp[c - 1] as Sign?;
                        }
                    }

                    c = 0;

                    List<int> i = new();
                    exp.Exp.ForEach(
                        _e =>
                        {
                            if (!(e is Sign))
                            {
                                Sign? _s = null;

                                if (c == 0)
                                {
                                    if (exp.Exp[c] is double)
                                    {
                                        _s = (double)exp.Exp[c] > 0 ? Sign.Plus : Sign.Minus;
                                    }
                                    else
                                    {
                                        _s = Sign.Plus;
                                    }
                                }
                                else
                                {
                                    if (exp.Exp[c] is double)
                                    {
                                        _s = exp.Exp[c - 1] as Sign?;
                                    }
                                }


                                if (e.Equals(_e) && s.Equals(_s))
                                    i.Add(c);
                                c++;
                            }
                        });

                    return i;
                }).Where(i => i.Count > 2).FirstOrDefault();

            if (indexes == null)
            {
                return false;
            }

            var obj = exp.Exp[indexes[0]];
            string sign = exp.Exp[indexes[1] - 1] as Sign? switch
            {
                Sign.Мultiply => "*",
                Sign.Division => "/",
                Sign.Plus => "+",
                Sign.Minus => "-",
                _ => throw new Exception("Not a Sign")
            };

            indexes.Reverse();
            indexes.ForEach(i =>
            {
                exp.Exp.RemoveAt(i);
                if (i > 0)
                {
                    exp.Exp.RemoveAt(i - 1);
                }
            });

            int index = exp.Exp.Count < indexes.Last() ? exp.Exp.Count : indexes.Last();
            if (index >= 0) index -= 1;

            Expression result = new Expression($"{(double)indexes.Count}*{obj}");
            result.Exp.Insert(0, _exp.Exp[indexes[1] - 1] as Sign?);

            exp.Exp.Insert(index, "<>");
            var solution = new VisualisationSolutionTransform(new List<Expression>() { result }, new Expression(exp.ToString()));
            solution.ToString();
            exp.Exp.RemoveAt(index);
            exp.Exp.InsertRange(index, result.Exp);

            Expression mainExp = new Expression(_exp.ToString());

            indexes.ForEach(i =>
            {
                mainExp.Exp.RemoveAt(i);
                mainExp.Exp.Insert(i, "<>");
                if (i > 0)
                {
                    mainExp.Exp.RemoveAt(i - 1);
                }
            });

            List<Expression> paramerets = new List<Expression>();

            indexes.ForEach(i =>
            {
                paramerets.Add(new Expression($"{sign}{obj}"));
            });

            string description = "";
            vis.Add(new VisualisationFrameTransform(paramerets, mainExp, solution, description));

            return true;
        }

        private bool RemoveZeroRule(Expression exp, List<IVisualisationFrame> vis)
        {
            if (!exp.Exp.Contains((double)0))
            {
                return false;
            }

            var _exp = new Expression(exp.ToString());

            int index;
            int count = 0;
            List<Expression> paramerets = new List<Expression>();

            while (true)
            {
                index = exp.Exp.IndexOf((double)0);

                if (index == -1)
                {
                    break;
                }

                count++;

                exp.Exp.RemoveAt(index);

                Sign? sign = null;

                if (index > 0)
                {
                    sign = exp.Exp[index - 1] as Sign?;
                    exp.Exp.RemoveAt(index - 1);
                }

                paramerets.Add(new Expression("0"));
                if (sign != null)
                {
                    paramerets.Last().Exp.Insert(0, sign);
                }
            }

            exp = new Expression(exp.ToString());
            var solution = new VisualisationSolutionTransform(new List<Expression>(), new Expression(exp.ToString()));

            for (int i = 0; i < count; i++)
            {
                int _index = _exp.Exp.IndexOf((double)0);

                _exp.Exp.RemoveAt(_index);

                if (_index > 0)
                {
                    _index -= 1;
                    _exp.Exp.RemoveAt(_index);
                }

                _exp.Exp.Insert(_index, "<>");
            }

            string description = "";
            vis.Add(new VisualisationFrameTransform(paramerets, _exp, solution, description));

            return true;
        }
    }
}
