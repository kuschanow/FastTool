using FastTool.CalculationTool.Interfaces;

namespace FastTool.CalculationTool.Operators;

public class Factor : Operator
{
    public Factor(ICalculateble op) : base(new ICalculateble[] { op }) { }

    public override double Calculate(Mode mode)
    {
        return new Functions.Factor(new ICalculateble[] { Operands[0] }).Calculate(mode);
    }

    public override string ToString() => $"{Operands[0]}!";
}
