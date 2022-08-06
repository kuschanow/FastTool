using System;

namespace FastTool;

public interface IFunction
{
    double Calculate(Mode mode, int digits);
}
