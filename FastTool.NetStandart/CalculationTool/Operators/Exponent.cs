using FastTool.CalculationTool.Functions;
using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Operators;

public class Exponent : Operator
{
    public Exponent(ICalculateble op1, ICalculateble op2) : base(new ICalculateble[] { op1, op2 }) { }

    public override Complex Calculate(Mode mode)
    {
        return new Pow(new ICalculateble[] { Operands[0], Operands[1] }).Calculate(mode);
    }

    public override string ToString() => $"{Operands[0]}^{Operands[1]}";
}
