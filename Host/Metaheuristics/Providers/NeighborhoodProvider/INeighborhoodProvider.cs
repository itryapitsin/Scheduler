using System.Collections.Generic;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.NeighborhoodProvider
{
    public interface INeighborhoodProvider<T>
    {
        IEnumerable<ISolution<T>> GetNeighbors(ISolution<T> solution);

        ISolution<T> GetBestNeighbor(ISolution<T> solution);

        ISolution<T> GetRandomNeighbor(ISolution<T> solution);

        string GetName();
    }
}
