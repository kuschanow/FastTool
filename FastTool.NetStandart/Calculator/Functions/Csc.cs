using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Csc : IFunction
{
    private readonly object arg;

    public Csc(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
