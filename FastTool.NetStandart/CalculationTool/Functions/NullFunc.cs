using FastTool.CalculationTool.Interfaces;
using System;

namespace FastTool.CalculationTool.Functions;

public class NullFunc : IFunction
{
    public string[] Names => new string[0];

    public ICalculateble[] Args => throw new NotImplementedException();

    public double Calculate(Mode mode) => throw new NotImplementedException();
}
