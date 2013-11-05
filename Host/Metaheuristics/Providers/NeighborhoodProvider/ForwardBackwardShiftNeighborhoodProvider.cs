using System;
using System.Collections.Generic;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.NeighborhoodProvider
{
    public class ForwardBackwardShiftNeighborhoodProvider<T> : INeighborhoodProvider<T>
    {
        public string GetName()
        {
            return "ForwardBackward shift neighborhood";
        }

        public ISolution<T> GetBestNeighbor(ISolution<T> solution)
        {
            ISolution<T> result = null;

            for (int i = 1; i <= solution.N; i++)
                for (int j = i + 1; j <= solution.N; j++)
                {
                    var item = solution.Clone();

                    item.Elements.Insert(1, item.Elements[i]);
                    item.Elements.RemoveAt(i + 1);

                    item.Elements.Insert(solution.N + 1, item.Elements[j]);
                    item.Elements.RemoveAt(j);

                    if (result == null || result.Cost > item.Cost) 
                        result = item;
                }

            return result ?? solution;
        }

        public IEnumerable<ISolution<T>> GetNeighbors(ISolution<T> solution)
        {
            for (var i = 1; i <= solution.N; i++)
                for (var j = i + 1; j <= solution.N; j++)
                {
                    var item = solution.Clone();

                    item.Elements.Insert(1, item.Elements[i]);
                    item.Elements.RemoveAt(i + 1);

                    item.Elements.Insert(solution.N + 1, item.Elements[j]);
                    item.Elements.RemoveAt(j);

                    yield return item;
                }
        }

        public ISolution<T> GetRandomNeighbor(ISolution<T> solution)
        {
            var result = solution.Clone();

            var rnd = new Random();
            int i = rnd.Next(solution.N) + 1;
            int j = rnd.Next(solution.N) + 1;

            result.Elements.Insert(1, result.Elements[i]);            
            result.Elements.RemoveAt(i + 1);

            result.Elements.Insert(solution.N + 1, result.Elements[j]);
            result.Elements.RemoveAt(j);

            return result;
        }
    }
}
