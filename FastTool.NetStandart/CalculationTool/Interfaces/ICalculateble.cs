using System.Numerics;

namespace FastTool.CalculationTool.Interfaces;

public interface ICalculateble
{
    Complex Calculate(Mode mode);
}
