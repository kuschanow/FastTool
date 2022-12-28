using FastTool.CalculationTool.Interfaces;
using System;
using System.Numerics;

namespace FastTool.CalculationTool.Functions;

public class Factor : IFunction
{
    public string[] Names => new string[] { "fact", "factor", "factorial" };

    public ICalculateble[] Args { get; }

    public Factor(ICalculateble[] args) => Args = args;

    public Complex Calculate(Mode mode)
    {
        Complex num = Args[0].Calculate(mode) + 1;

        return Gamma(num);
    }

    private Complex GammaApprox(Complex x)
    {
        double[] p = { -1.71618513886549492533811e+0, 2.47656508055759199108314e+1, -3.79804256470945635097577e+2, 6.29331155312818442661052e+2, 8.66966202790413211295064e+2, -3.14512729688483675254357e+4, -3.61444134186911729807069e+4, 6.64561438202405440627855e+4 };

        double[] q = { -3.08402300119738975254353e+1, 3.15350626979604161529144e+2, -1.01515636749021914166146e+3, -3.10777167157231109440444e+3, 2.25381184209801510330112e+4, 4.75584627752788110767815e+3, -1.34659959864969306392456e+5, -1.15132259675553483497211e+5 };
        Complex z = x - 1.0;
        Complex a = 0.0;
        Complex b = 1.0;
        int i;
        for (i = 0; i < 8; i++)
        {
            a = (a + p[i]) * z;
            b = b * z + q[i];
        }
        return (a / b + 1.0);
    }

    private Complex Gamma(Complex z)
    {

        if ((z.Real > 0) && (z.Real < 1.0))
        {
            return Gamma(z + 1.0) / z;
        }

        if (z.Real > 2)
        {
            return (z - 1) * Gamma(z - 1);
        }

        if (z.Real <= 0)
        {
            return Math.PI / (Complex.Sin(Math.PI * z) * Gamma(1 - z));
        }

        return GammaApprox(z);
    }

    public override string ToString() => $"fact({Args[0]})";
}
