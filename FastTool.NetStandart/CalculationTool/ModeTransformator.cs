using System;

namespace FastTool.CalculationTool;

public static class ModeTransformator
{
    public static double ToRad(double num, Mode mode)
    {
        return mode switch
        {
            Mode.Deg => num *= Math.PI / 180,
            Mode.Rad => num,
            Mode.Grad => num *= Math.PI / 200,
            _ => num,
        };
    }
    public static double FromRad(double num, Mode mode)
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
