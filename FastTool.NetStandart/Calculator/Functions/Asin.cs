using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asin : IFunction
{
    private readonly object arg;

    public Asin(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
