using System;
using System.Collections.Generic;

namespace FastTool;

public interface IFunction
{
    List<object> Args { get; }
    double Calculate(Calculator calc);
    double Calculate(Mode mode, int digits);
}
