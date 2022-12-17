using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Operators;

public class Operator : IOperator
{
    public ICalculateble[] Operands { get; private set; }

    public Operator(ICalculateble[] operands) => Operands = operands;

    public virtual double Calculate(Mode mode)
    {
        throw new NotImplementedException();
    }
}
