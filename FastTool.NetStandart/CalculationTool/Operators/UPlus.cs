using FastTool.CalculationTool.Interfaces;
using System.Numerics;

namespace FastTool.CalculationTool.Operators;

public class UPlus : Operator
{
    public UPlus(ICalculateble op) : base(new ICalculateble[] { op }) { }

    public override Complex Calculate(Mode mode)
    {
        var complex = Operands[0].Calculate(mode);
        return new Complex(+complex.Real, complex.Imaginary);
    }

    public override string ToString() => $"+{Operands[0]}";
}
