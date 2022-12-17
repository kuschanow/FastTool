using FastTool.CalculationTool.Interfaces;

namespace FastTool.CalculationTool.Operators;

public class UPlus : Operator
{
    public UPlus(ICalculateble op) : base(new ICalculateble[] { op }) { }

    public override double Calculate(Mode mode)
    {
        return +Operands[0].Calculate(mode);
    }

    public override string ToString() => $"+{Operands[0]}";
}
