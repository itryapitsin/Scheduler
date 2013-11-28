using System;
using Metaheuristics.Solutions;

namespace Metaheuristics.Algorithms
{
    public interface ISolver<T>
    {
        //ISolution Solve(TimeSpan? timeLimit, object[] parameters = null);
        ISolution<T> Solve(TimeSpan timeLimit, ISolution<T> initialSolution = null);
    }
}
