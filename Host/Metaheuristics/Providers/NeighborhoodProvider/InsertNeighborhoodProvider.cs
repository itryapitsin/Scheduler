using System;
using System.Collections.Generic;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.NeighborhoodProvider
{
    public class InsertNeighborhoodProvider<T> : INeighborhoodProvider<T>
    {
        public string GetName()
        {
            return "Insert neighborhood";
        }

        public ISolution<T> GetBestNeighbor(ISolution<T> solution)
        {
            ISolution<T> result = null;

            for (int i = 1; i <= solution.N; i++)
                for (int j = i + 1; j <= solution.N; j++)
                {
                    var item = solution.Clone();

                    if (i < j)
                    {
                        item.Elements.Insert(j, item.Elements[i]);
                        item.Elements.RemoveAt(i);
                    }
                    else
                    {
                        item.Elements.Insert(j, item.Elements[i]);
                        item.Elements.RemoveAt(i + 1);
                    }

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

                    if (i < j)
                    {
                        item.Elements.Insert(j, item.Elements[i]);
                        item.Elements.RemoveAt(i);
                    }
                    else
                    {
                        item.Elements.Insert(j, item.Elements[i]);
                        item.Elements.RemoveAt(i + 1);
                    }

                    yield return item;
                }
        }

        public ISolution<T> GetRandomNeighbor(ISolution<T> solution)
        {
            var result = solution.Clone();

            var rnd = new Random();
            int i = rnd.Next(solution.N) + 1;
            int j = rnd.Next(solution.N) + 1;

            if (i < j)
            {
                result.Elements.Insert(j, result.Elements[i]);
                result.Elements.RemoveAt(i);
            }
            else
            {
                result.Elements.Insert(j, result.Elements[i]);
                result.Elements.RemoveAt(i + 1);
            }
            return result;
        }
    }
}
