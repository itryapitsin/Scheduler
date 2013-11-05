using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.CostProvider
{
    public interface ICostProvider<T>
    {
        double GetCost(ISolution<T> solution);
    }
}
