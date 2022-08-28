namespace FastTool
{
    public record VisualisationSolution
    {
        public double Answer { get; }
        public Expression MainExp { get; }
        public Visualisation Detail { get; }

        public VisualisationSolution(double answer, Expression mainExp)
        {
            if (answer < 0 && mainExp.Exp.IndexOf("<>") > 0 && mainExp.Exp[mainExp.Exp.IndexOf("<>") - 1] is Sign)
            {
                if (mainExp.Exp[mainExp.Exp.IndexOf("<>") - 1] is Sign.Minus)
                {
                    MainExp = new Expression(mainExp.ToString().Replace("-<>", "+<>"));
                }
                if (mainExp.Exp[mainExp.Exp.IndexOf("<>") - 1] is Sign.Plus)
                {
                    MainExp = new Expression(mainExp.ToString().Replace("+<>", "-<>"));
                }
                Answer = answer * -1;
            }
            else
            {
                Answer = answer;
                MainExp = mainExp;
            }
        }

        public VisualisationSolution(double answer, Expression mainExp, Visualisation detail) : this(answer, mainExp)
        {
            Detail = detail;
        }

        public override string ToString()
        {
            string str = "";

            str = MainExp.ToString();

            str = str.Replace("<>", Answer.ToString());

            return str;
        }
    }
}
