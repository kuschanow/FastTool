using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Pow : IFunction
{
    private readonly object firstArg;
    private readonly object secondArg;

    public Pow(object firstArg, object secondArg)
    {
        this.firstArg = firstArg;
        this.secondArg = secondArg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
