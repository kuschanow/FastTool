using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Sin : IFunction
{
    private readonly object arg;

    public Sin(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
