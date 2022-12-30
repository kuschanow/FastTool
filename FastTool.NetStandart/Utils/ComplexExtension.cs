using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FastTool.Utils
{
    public static class ComplexExtension
    {
        public static string ToStringSmart(this Complex complex)
        {
            string str = "";

            if (complex.Imaginary == 0)
            {
                str = $"{complex.Real}";
            }
            else
            {
                str = $"{complex.Real} {(complex.Imaginary > 0 ? '+' : '-')} {complex.Imaginary}i";
            }

            return str;
        }

        public static Complex Round(this Complex complex, int digit)
        {
            if (digit > 15)
                return new Complex(complex.Real, complex.Imaginary);
            else
                return new Complex(Math.Round(complex.Real, digit), Math.Round(complex.Imaginary, digit));

        }
    }
}
