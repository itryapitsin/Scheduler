namespace Metaheuristics.Solutions
{
    public interface ISolutionBuilder<T>
    {
        ISolution<T> BuildSolution();
    }    
}
