using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Acot : IFunction
{
    private readonly object arg;

    public Acot(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
