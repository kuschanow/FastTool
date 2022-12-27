using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Operators;

public class UMinus : Operator
{
    public UMinus(ICalculateble op) : base(new ICalculateble[] { op }) { }

    public override Complex Calculate(Mode mode)
    {
        return -Operands[0].Calculate(mode);
    }

    public override string ToString() => $"-{Operands[0]}";
}
