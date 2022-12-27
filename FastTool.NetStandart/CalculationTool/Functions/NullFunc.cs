using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class NullFunc : IFunction
{
    public string[] Names => new string[0];

    public ICalculateble[] Args => throw new NotImplementedException();

    public Complex Calculate(Mode mode) => throw new NotImplementedException();
}
