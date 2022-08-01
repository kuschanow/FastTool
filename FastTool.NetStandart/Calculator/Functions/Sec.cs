using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Sec : IFunction
{
    private readonly object arg;

    public Sec(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
