using System.Collections.Generic;
using System.ComponentModel;

namespace FastTool
{
    public record VisualisationFrameTransform : IVisualisationFrame
    {
        public List<Expression> Parameters { get; }
        public Expression MainExp { get; }
        public IVisualisationSolution Solution { get; }
        public string Description { get; } 

        public VisualisationFrameTransform(List<Expression> parametrs, Expression mainExp, VisualisationSolutionTransform solution, string description)
        {
            Parameters = parametrs;
            MainExp = mainExp;
            Solution = solution;
            Description = description;
        }

        public override string ToString()
        {
            string str = "";

            str = MainExp.ToString();

            int i = 0;

            while (i < Parameters.Count)
            {
                int index = str.IndexOf("<>");

                str = str.Remove(index, 2);
                str = str.Insert(index, Parameters[i].ToString());

                i++;
            }

            return str;
        }

    }
}
