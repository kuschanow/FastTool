﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public class Acsc : IFunction
{
    private readonly object arg;

    public Acsc(object arg)
    {
        this.arg = arg;
    }

    public double Calculate()
    {
        throw new NotImplementedException();
    }
}