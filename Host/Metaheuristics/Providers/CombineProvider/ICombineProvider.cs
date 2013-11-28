using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.CombineProvider
{
    public interface ICombineProvider<T>
    {
        ISolution<T> Combine(ISolution<T> solutionFirst, ISolution<T> solutionSecond);

        string GetName();
    }
}
