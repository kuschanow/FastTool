using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Asec : IFunction
{
    private readonly object arg;

    public Asec(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}
