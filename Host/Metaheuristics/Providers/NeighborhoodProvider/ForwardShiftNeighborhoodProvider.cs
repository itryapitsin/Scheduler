using System;
using System.Collections.Generic;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.NeighborhoodProvider
{
    public class ForwardShiftNeighborhoodProvider<T> : INeighborhoodProvider<T>
    {
        public string GetName()
        {
            return "Forward shift neighborhood";
        }

        public ISolution<T> GetBestNeighbor(ISolution<T> solution)
        {
            ISolution<T> result = null;

            for (int i = 2; i <= solution.N; i++)
            {
                var item = solution.Clone();

                item.Elements.Insert(1, item.Elements[i]);
                item.Elements.RemoveAt(i + 1);

                if (result == null || result.Cost > item.Cost) 
                    result = item;
            }

            return result ?? solution;
        }

        public IEnumerable<ISolution<T>> GetNeighbors(ISolution<T> solution)
        {
            for (int i = 2; i <= solution.N; i++)
            {
                var item = solution.Clone();

                item.Elements.Insert(1, item.Elements[i]);
                item.Elements.RemoveAt(i + 1);

                yield return item;
            }
        }

        public ISolution<T> GetRandomNeighbor(ISolution<T> solution)
        {
            var result = solution.Clone();

            var rnd = new Random();
            int i = rnd.Next(solution.N - 1) + 2;

            result.Elements.Insert(1, result.Elements[i]);
            result.Elements.RemoveAt(i + 1);

            return result;
        }
    }
}
