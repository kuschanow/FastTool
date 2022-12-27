using System;
using System.Numerics;

namespace FastTool.CalculationTool;

public static class ModeTransformator
{
    public static Complex ToRad(Complex num, Mode mode)
    {
        return mode switch
        {
            Mode.Deg => num *= Math.PI / 180,
            Mode.Rad => num,
            Mode.Grad => num *= Math.PI / 200,
            _ => num,
        };
    }
    public static Complex FromRad(Complex num, Mode mode)
    {
        return mode switch
        {
            Mode.Deg => num /= Math.PI / 180,
            Mode.Rad => num,
            Mode.Grad => num /= Math.PI / 200,
            _ => num,
        };
    }
}
