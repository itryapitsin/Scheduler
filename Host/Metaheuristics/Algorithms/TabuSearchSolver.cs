using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Metaheuristics.Solutions;

namespace Metaheuristics.Algorithms
{
    public class TabuSearchSolver<T> : ISolver<T>
    {
        private readonly ISolutionBuilder<T> _solutionBuilder;

        public TabuSearchSolver(ISolutionBuilder<T> solutionBuilder)
        {
            _solutionBuilder = solutionBuilder;
        }

        /// <summary>
        /// Find a solution
        /// </summary>
        /// <returns></returns>
        public ISolution<T> Solve(TimeSpan timeLimit, ISolution<T> initialSolution)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            ISolution<T> bestSolution;
            if (initialSolution != null)
            {
                bestSolution = initialSolution;
            }
            else
            {
                bestSolution = _solutionBuilder.BuildSolution();
                bestSolution.FillWithRandomData();
            }

            int tabuMaxCount = bestSolution.N;

            ISolution<T> currentSolution = bestSolution;
            ISolution<T> nextSolution;

            var tabu = new HashSet<ISolution<T>>();

            bool stop;

            do
            {
                nextSolution = currentSolution;
                var neighbors = currentSolution.GetNeighbors();
                stop = true;
                // Find best neighbor
                foreach (var item in neighbors)
                {
                    // current move is not allowed
                    bool moveAllowed = (tabu.Any(x => (x.IsEqual(item))) == false);

                    if (moveAllowed)
                    {
                        if (item.Cost > 0 && (nextSolution.Cost < 0 || nextSolution.Cost > item.Cost))
                        {
                            nextSolution = item;
                            stop = false;
                        }
                    }
                }

                if (stop == true)
                {
                    currentSolution = _solutionBuilder.BuildSolution();
                    currentSolution.FillWithRandomData();
                }

                if (tabu.Count > tabuMaxCount && tabu.Count > 0)
                    tabu.Remove(tabu.First());
                tabu.Add(nextSolution);

                if (bestSolution.Cost < 0 || bestSolution.Cost > nextSolution.Cost) bestSolution = nextSolution;
                currentSolution = nextSolution;
            } while (stopWatch.Elapsed < timeLimit);

            stopWatch.Stop();

            return bestSolution;
        }
    }
}
