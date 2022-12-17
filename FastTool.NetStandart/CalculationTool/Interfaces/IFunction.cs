namespace FastTool.CalculationTool.Interfaces;

public interface IFunction : ICalculateble
{
    string[] Names { get; }
    ICalculateble[] Args { get; }
}
