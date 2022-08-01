using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Atan : IFunction
{
    private readonly object arg;

    public Atan(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
