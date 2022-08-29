using System.Collections.Generic;

namespace FastTool
{
    public class VisualisationSolutionTransform : IVisualisationSolution
    {
        public List<Expression> Results { get; }
        public Expression MainExp { get; }
        public Visualisation Detail { get; }

        public VisualisationSolutionTransform(List<Expression> results, Expression mainExp)
        {
            Results = results;
            MainExp = mainExp;
        }

        public VisualisationSolutionTransform(List<Expression> results, Expression mainExp, Visualisation detail) : this(results, mainExp)
        {
            Detail = detail;
        }

        public override string ToString()
        {
            string str = "";

            str = MainExp.ToString();

            int i = 0;

            while (i < Results.Count)
            {
                int index = str.IndexOf("<>");

                str = str.Remove(index, 2);
                str = str.Insert(index, Results[i].ToString());

                i++;
            }

            return str;
        }
    }
}