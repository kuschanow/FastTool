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
                if (LikeTermsRule(ref exp, ref vis))
                {
                    continue;
                }

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

        private bool ActionRule(Sign first, Sign second, ref Expression exp, ref List<IVisualisationFrame> vis)
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

            var solution = new VisualisationSolutionCalc(answer, new Expression(exp.ToString()));
            string description = "";
            vis.Add(new VisualisationFrameCalc(currAction, new Expression(exp.ToString()), solution, description));

            exp.Exp.Remove("<>");
            exp.Exp.Insert(index - 1, answer);

            return true;
        }

        private bool ElementRule(object obj, int index, ref Expression exp, ref List<IVisualisationFrame> vis)
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

        private bool FuncRule(IFunction func, ref Expression exp, ref List<IVisualisationFrame> vis)
        {
            if (exp.Exp.Select(e => (e as IFunction) != null).Count() == 0)
            {
                return false;
            }

            List<IVisualisationFrame> _vis = new List<IVisualisationFrame>(); 
            IFunction _func = new Expression(func.ToString()).Exp[0] as IFunction;

            object obj1, obj2;

            (obj1, obj2) = (func.Args[0], func.Args.Count == 2 ? func.Args[1] : null);

            FuncArgRule(obj1, 0, ref _func, ref _vis);
            FuncArgRule(obj2, 1, ref _func, ref _vis);

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

        private bool FuncArgRule(object obj, int index, ref IFunction func, ref List<IVisualisationFrame> vis)
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

        private bool LikeTermsRule(ref Expression exp, ref List<IVisualisationFrame> vis)
        {
            var _exp = new Expression(exp.ToString());

            var indexes = _exp.Exp.Select((e, c) =>
                {
                    if (e is Sign)
                    {
                        return new List<int>();
                    }

                    Sign? s = null;

                    if (c == 0)
                    {
                        if (_exp.Exp[c] is double)
                        {
                            s = (double)_exp.Exp[c] > 0 ? Sign.Plus : Sign.Minus;
                        }
                        else
                        {
                            s = Sign.Plus;
                        }
                    }
                    else
                    {
                        if (_exp.Exp[c] is double)
                        {
                            s = _exp.Exp[c - 1] as Sign?;
                        }
                    }

                    c = 0;

                    List<int> i = new();
                    _exp.Exp.ForEach(
                        _e =>
                        {
                            if (!(e is Sign))
                            {
                                Sign? _s = null;

                                if (c == 0)
                                {
                                    if (_exp.Exp[c] is double)
                                    {
                                        _s = (double)_exp.Exp[c] > 0 ? Sign.Plus : Sign.Minus;
                                    }
                                    else
                                    {
                                        _s = Sign.Plus;
                                    }
                                }
                                else
                                {
                                    if (_exp.Exp[c] is double)
                                    {
                                        _s = _exp.Exp[c - 1] as Sign?;
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

            var obj = _exp.Exp[indexes[0]];
            string sign = _exp.Exp[indexes[1] - 1] as Sign? switch
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
                _exp.Exp.RemoveAt(i);
                if (i > 0)
                {
                    _exp.Exp.RemoveAt(i - 1);
                }
            });

            int index = _exp.Exp.Count < indexes.Last() ? _exp.Exp.Count : indexes.Last();
            if (index >= 0) index -= 1;

            Expression result = new Expression($"{(double)indexes.Count}*{obj}");
            result.Exp.Insert(0, exp.Exp[indexes[1] - 1] as Sign?);

            _exp.Exp.Insert(index, "<>");
            var solution = new VisualisationSolutionTransform(new List<Expression>() { result }, new Expression(_exp.ToString()));
            solution.ToString();
            _exp.Exp.RemoveAt(index);
            _exp.Exp.InsertRange(index, result.Exp);

            Expression mainExp = new Expression(exp.ToString());

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

            exp = _exp;

            return true;
        }    
    }
}
