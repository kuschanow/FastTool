namespace FastTool.CalculationTool.Interfaces
{
    public interface IOperator : ICalculateble
    {
        ICalculateble[] Operands { get; }
    }
}
