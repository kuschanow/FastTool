namespace FastTool
{
    public record VisualisationFrame
    {
        public Expression CurrAction { get; }
        public Expression MainExp { get; }
        public VisualisationSolution Solution { get; }
        public bool Main { get; }

        public VisualisationFrame(Expression currAction, Expression mainExp, VisualisationSolution solution, bool main = true)
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
            Main = main;
        }

        public override string ToString()
        {
            string str = "";

            str = MainExp.ToString();

            if (Main)
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
