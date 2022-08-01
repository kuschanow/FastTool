using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Tan : IFunction
{
    private readonly object arg;

    public Tan(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
