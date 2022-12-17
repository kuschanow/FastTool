namespace FastTool.CalculationTool.Interfaces;

public interface IConst : ICalculateble
{
    string[] Names { get; }
    double Value { get; }
}
