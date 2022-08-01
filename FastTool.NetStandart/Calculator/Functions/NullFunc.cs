using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class NullFunc : IFunction
{
    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
