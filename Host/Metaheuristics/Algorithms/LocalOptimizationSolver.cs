using System;
using System.Diagnostics;
using Metaheuristics.Solutions;

namespace Metaheuristics.Algorithms
{
    public class LocalOptimizationSolver<T> : ISolver<T>
    {
        private readonly ISolutionBuilder<T> _solutionBuilder;

        public LocalOptimizationSolver(ISolutionBuilder<T> solutionBuilder)
        {
            _solutionBuilder = solutionBuilder;
        }       

        /// <summary>
        /// Find a solution
        /// </summary>
        /// <returns></returns>
        public ISolution<T> Solve(TimeSpan timeLimit, ISolution<T> initialSolution = null)
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

            ISolution<T> currentSolution = bestSolution;
            ISolution<T> nextSolution;

            while (stopWatch.Elapsed < timeLimit)
            {
                do
                {
                    nextSolution = currentSolution.GetBestNeighbor();
                    if (nextSolution.Cost > currentSolution.Cost)
                        break;

                    if (bestSolution.Cost < 0 || bestSolution.Cost > nextSolution.Cost) bestSolution = nextSolution;

                    currentSolution = nextSolution;
                } while (stopWatch.Elapsed < timeLimit);

                currentSolution = _solutionBuilder.BuildSolution();
                currentSolution.FillWithRandomData();
            } 

            stopWatch.Stop();

            return bestSolution;
        }
    }
}
