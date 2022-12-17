using FastTool.CalculationTool.Operators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FastTool.CalculationTool;

public class ExpEnumerator : IEnumerator
{
    private List<Operator> operators;
    private int position = -1;

    public object Current
    {
        get
        {
            if (position < 0 || position >= operators.Count)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            return operators[position];
        }
    }

    public ExpEnumerator(List<Operator> operators) => this.operators = operators.OrderBy(o => o).ToList();

    public bool MoveNext() => position++ < operators.Count - 1;

    public void Reset() => position = -1;
}
