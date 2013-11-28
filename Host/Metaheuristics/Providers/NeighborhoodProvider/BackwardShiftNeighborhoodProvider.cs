using System;
using System.Collections.Generic;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.NeighborhoodProvider
{
    public class BackwardShiftNeighborhoodProvider<T> : INeighborhoodProvider<T>
    {
        public string GetName()
        {
            return "Backward shift neighborhood";
        }

        public ISolution<T> GetBestNeighbor(ISolution<T> solution)
        {
            ISolution<T> result = null;

            for (int i = 2; i <= solution.N; i++)
                for (int j = 1; j <= i - 1; j++)
                {
                    var item = solution.Clone();

                    item.Elements.Insert(i - j, item.Elements[i]);
                    item.Elements.RemoveAt(i + 1);

                    if (result == null || result.Cost > item.Cost) result = item;
                }

            return result ?? solution;
        }

        public IEnumerable<ISolution<T>> GetNeighbors(ISolution<T> solution)
        {
            for (int i = 2; i <= solution.N; i++)
                for (int j = 1; j <= i - 1; j++)
                {
                    var item = solution.Clone();

                    item.Elements.Insert(i - j, item.Elements[i]);
                    item.Elements.RemoveAt(i + 1);

                    yield return item;
                }
        }

        public ISolution<T> GetRandomNeighbor(ISolution<T> solution)
        {
            var result = solution.Clone();

            var rnd = new Random();
            int i = rnd.Next(solution.N - 1) + 2;
            int j = rnd.Next(i - 1) + 1;

            result.Elements.Insert(i - j, result.Elements[i]);
            result.Elements.RemoveAt(i + 1);

            return result;
        }
    }
}
