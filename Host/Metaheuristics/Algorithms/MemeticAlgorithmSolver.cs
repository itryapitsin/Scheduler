using System;
using System.Collections.Generic;
using System.Diagnostics;
using Metaheuristics.Solutions;

namespace Metaheuristics.Algorithms
{
    public class MemeticAlgorithmSolver<T> : ISolver<T>
    {
        private readonly ISolutionBuilder<T> _solutionBuilder;

        public MemeticAlgorithmSolver(ISolutionBuilder<T> solutionBuilder)
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

            int generationLength = bestSolution.N;

            var generation = new List<ISolution<T>>() { bestSolution };

            for (int i = 0; i < generationLength - 1; i++)
            {
                ISolution<T> item = _solutionBuilder.BuildSolution();
                item.FillWithRandomData();
                generation.Add(item);
            }

            Random rnd = new Random();
            //int count = 0;
            do
            {
                //bool found = false;
                foreach (var item in generation)
                    if (bestSolution.Cost < 0 || bestSolution.Cost > item.Cost)
                    {
                        bestSolution = item;
                        //found = true;
                    }
                //Debug.WriteLine(found.ToString());

                for (int i = 0; i < generationLength - 1; i++)
                {
                    ISolution<T> child = generation[i].Combine(generation[i + 1]);
                    generation.Add(child.GetRandomNeighbor());

                    var neighbors = child.GetNeighbors();
                    ISolution<T> localBestSolution = child;

                    // Find best neighbor
                    foreach (var item in neighbors)
                    {
                        if (item.Cost > 0)
                        {
                            if (localBestSolution.Cost < 0 || localBestSolution.Cost > item.Cost)
                                localBestSolution = item;
                        }
                    }

                    generation.Add(localBestSolution);

                }

                if (stopWatch.Elapsed > timeLimit) break;
                generation.Sort((x, y) => (x.Cost < y.Cost ? -1 : 1));

                if (stopWatch.Elapsed > timeLimit) break;
                // Remove 
                for (int i = generation.Count - 1; i >= generationLength; i--)
                    generation.RemoveAt(i);

                //count++;
            } while (stopWatch.Elapsed < timeLimit);
            stopWatch.Stop();

            return bestSolution;
        }
    }
}