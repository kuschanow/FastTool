namespace FastTool
{
    public record VisualisationFrameCalc : IVisualisationFrame
    {
        public Expression CurrAction { get; }
        public Expression MainExp { get; }
        public IVisualisationSolution Solution { get; }
        public bool MainAction { get; }
        public string Description { get; }

        public VisualisationFrameCalc(Expression currAction, Expression mainExp, VisualisationSolutionCalc solution, string description, bool main = true)
        {
            if (currAction.Exp[0] as IFunction == null && (double)currAction.Exp[0] < 0 && mainExp.Exp.IndexOf("<>") > 0 && mainExp.Exp[mainExp.Exp.IndexOf("<>") - 1] is Sign)
            {
                if (mainExp.Exp[mainExp.Exp.IndexOf("<>") - 1] is Sign.Minus)
                {
                    MainExp = new Expression(mainExp.ToString().Replace("-<>", "+<>"));
                }
                if (mainExp.Exp[mainExp.Exp.IndexOf("<>") - 1] is Sign.Plus)
                {
                    MainExp = new Expression(mainExp.ToString().Replace("+<>", "-<>"));
                }
                CurrAction = new Expression(currAction.ToString().TrimStart('-'));
            }
            else
            {
                CurrAction = currAction;
                MainExp = mainExp;
            }
            Solution = solution;
            MainAction = main;
            Description = description;
        }

        public override string ToString()
        {
            string str = "";

            str = MainExp.ToString();

            if (MainAction)
            {
                str = str.Replace("<>", CurrAction.ToString());
            }
            else
            {
                str = str.Replace("<>", $"({CurrAction})");
            }

            return str;
        }
    }
}
