using System;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace FastTool.Utils
{
    public static class ComplexExtension
    {
        public static string ToStringSmart(this Complex complex, int exp, int digit)
        {
            string str;
            string formatReal, formatIm;

            string roundFormat = string.Join("", Enumerable.Range(0, digit).Select(i => '#'));

            if (exp < 16 && ((int)complex.Real).ToString().Length > exp)
                formatReal = $"0.{roundFormat}E0";
            else
                formatReal = $"0.{roundFormat}";

            if (exp < 16 && ((int)complex.Imaginary).ToString().Length > exp)
                formatIm = $"0.{roundFormat}E0";
            else
                formatIm = $"0.{roundFormat}";


            if (complex.Imaginary == 0)
                str = complex.Real.ToString(formatReal, new CultureInfo("en-US"));
            else
                str = $"{complex.Real.ToString(formatReal, new CultureInfo("en-US"))} {(complex.Imaginary > 0 ? '+' : '-')} {complex.Imaginary.ToString(formatIm, new CultureInfo("en-US"))}i";

            return str;
        }
    }
}
