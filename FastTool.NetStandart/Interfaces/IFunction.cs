using System;
using System.Collections.Generic;

namespace FastTool;

public interface IFunction
{
    double Calculate(Calculator calc);
    double Calculate(Mode mode, int digits);
}
