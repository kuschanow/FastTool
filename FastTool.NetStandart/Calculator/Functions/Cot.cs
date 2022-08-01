using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Cot : IFunction
{
    private readonly object arg;

    public Cot(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
