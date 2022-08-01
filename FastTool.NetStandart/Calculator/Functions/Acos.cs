using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Acos : IFunction
{
    private readonly object arg;

    public Acos (object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
