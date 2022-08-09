using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class NullFunc : IFunction
{
    public double Calculate(Calculator calc)
    {
        throw new NotImplementedException();
    }

    public double Calculate(Mode mode, int digits)
    {
        throw new NotImplementedException();
    }
}
