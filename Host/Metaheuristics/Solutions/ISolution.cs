using System.Collections.Generic;

namespace Metaheuristics.Solutions
{
    public interface ISolution<T>
    {
        int N { get; set; }
        List<T> Elements { get; set; }

        double Cost { get; set; }

        ISolution<T> Combine(ISolution<T> solution);
        ISolution<T> GetBestNeighbor();
        ISolution<T> GetRandomNeighbor();
        IEnumerable<ISolution<T>> GetNeighbors();

        ISolution<T> Clone();
        bool IsEqual(ISolution<T> solution);

        void FillWithRandomData();
    }
}
