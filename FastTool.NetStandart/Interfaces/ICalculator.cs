using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool;

public interface ICalculator
{
    double Calculate(Expression exp);
    double ConvertToRad(double num);
    double ConvertFromRad(double num);

}
