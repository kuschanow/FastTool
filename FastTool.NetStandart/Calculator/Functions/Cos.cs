using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Cos : IFunction
{
    private readonly object arg;

    public Cos(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
