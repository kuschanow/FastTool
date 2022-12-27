using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Operators;

public class Operator : IOperator
{
    public ICalculateble[] Operands { get; private set; }

    public Operator(ICalculateble[] operands) => Operands = operands;

    public virtual Complex Calculate(Mode mode)
    {
        throw new NotImplementedException();
    }
}
