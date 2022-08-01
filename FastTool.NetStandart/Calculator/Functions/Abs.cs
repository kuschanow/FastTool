using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Abs : IFunction
{
    private readonly object arg;

    public Abs(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
