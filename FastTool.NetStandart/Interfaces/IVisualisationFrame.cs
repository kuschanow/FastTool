namespace FastTool
{
    public interface IVisualisationFrame
    {
        Expression MainExp { get; }
        IVisualisationSolution Solution { get; }
        string Description { get; }
    }
}