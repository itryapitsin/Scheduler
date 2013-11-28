using System;
using System.Collections.Generic;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.NeighborhoodProvider
{
    public class OneSwapNeighborhoodProvider<T> : INeighborhoodProvider<T>
    {
        public string GetName()
        {
            return "One swap neighborhood";
        }

        public ISolution<T> GetBestNeighbor(ISolution<T> solution)
        {
            ISolution<T> result = null;

            for (int i = 1; i < solution.N; i++)
                for (int j = i + 1; j <= solution.N; j++)
                {
                    ISolution<T> item = solution.Clone();
                    var t = item.Elements[i];
                    item.Elements[i] = item.Elements[j];
                    item.Elements[j] = t;

                    if (result == null || result.Cost > item.Cost) result = item;
                }

            if (result == null) 
                result = solution;
            return result;
        }

        public IEnumerable<ISolution<T>> GetNeighbors(ISolution<T> solution)
        {
            for (int i = 1; i < solution.N; i++)
                for (int j = i + 1; j <= solution.N; j++)
                {
                    ISolution<T> item = solution.Clone();
                    var t = item.Elements[i];
                    item.Elements[i] = item.Elements[j];
                    item.Elements[j] = t;

                    yield return item;
                }
        }

        public ISolution<T> GetRandomNeighbor(ISolution<T> solution)
        {
            var result = solution.Clone();

            Random rnd = new Random();
            int i = rnd.Next(solution.N) + 1;
            int j = rnd.Next(solution.N) + 1;

            var t = result.Elements[i];
            result.Elements[i] = result.Elements[j];
            result.Elements[j] = t;

            return result;
        }
    }
}
