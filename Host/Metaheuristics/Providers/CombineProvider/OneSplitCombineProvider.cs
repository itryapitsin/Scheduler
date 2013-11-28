using System;
using Metaheuristics.Solutions;

namespace Metaheuristics.Providers.CombineProvider
{
    public class OneSplitCombineProvider<T> : ICombineProvider<T>
    {
        public string GetName()
        {
            return "One Split Combine";
        }

        public ISolution<T> Combine(ISolution<T> solutionFirst, ISolution<T> solutionSecond)
        {
            Random rnd = new Random();
            ISolution<T> result = solutionFirst.Clone();

            int k = rnd.Next(solutionFirst.N) + 1;

            for (int i = 1; i <= k; i++)
                result.Elements[i] = solutionFirst.Elements[i];

            for (int i = 1; i <= solutionSecond.N; i++)
                if (!result.Elements.Contains(solutionSecond.Elements[i]))
                {
                    result.Elements[k] = solutionSecond.Elements[i];
                    k++;
                }
            return result;
        }
    }
}
