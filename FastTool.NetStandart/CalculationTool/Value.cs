using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool;

public class Value : ICalculateble
{
    public string Name { get; init; }
    public ICalculateble Expression {get; init; }

    public Value(string name, ICalculateble expression)
    {
        Name = name;
        Expression = expression;
    }

    public Complex Calculate(Mode mode)
    {
        return Expression.Calculate(mode);
    }

    public override string ToString() => Name;
}
