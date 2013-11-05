using System;
using System.Diagnostics;
using Metaheuristics.Solutions;

namespace Metaheuristics.Algorithms
{
    public class SimulatedAnnealingSolver<T> : ISolver<T>
    {
        private ISolutionBuilder<T> _solutionBuilder;
        private double _initTemperature;
        private double _coolingRate;
        private double _minTemperature;

        public SimulatedAnnealingSolver(ISolutionBuilder<T> solutionBuilder, double initialTemperature, double coolingRate, double minTemperature)
        {
            _solutionBuilder = solutionBuilder;
            _initTemperature = initialTemperature;
            _coolingRate = coolingRate;
            _minTemperature = minTemperature;
        }

        /// <summary>
        /// Find a solution
        /// </summary>
        /// <returns></returns>
        public ISolution<T> Solve(TimeSpan timeLimit, ISolution<T> initialSolution)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Random rnd = new Random();

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

            ISolution<T> currentSolution;
            ISolution<T> nextSolution;

            int count = 1;
            while (stopWatch.Elapsed < timeLimit)
            {
                currentSolution = bestSolution;
                double temperature = _initTemperature / count;
                count++;

                do
                {
                    nextSolution = null;
                    ISolution<T> item = currentSolution.GetRandomNeighbor();

                    if (item.Cost <= currentSolution.Cost)
                    {
                        nextSolution = item;
                        if (bestSolution.Cost < 0 || bestSolution.Cost > nextSolution.Cost)
                            bestSolution = nextSolution;
                    }
                    else
                    {
                        double delta = item.Cost - currentSolution.Cost;
                        double exp = Math.Exp(-(double)delta / temperature);
                        if (rnd.NextDouble() < exp)
                        {
                            nextSolution = item;
                            //if (bestSolution.Cost < 0 || bestSolution.Cost > nextSolution.Cost)
                            //    bestSolution = nextSolution;
                        }
                    }

                    if (nextSolution != null) currentSolution = nextSolution;
                    temperature = temperature * _coolingRate;
                } while (stopWatch.Elapsed < timeLimit && temperature > _minTemperature);
            }
            stopWatch.Stop();

            return bestSolution;
        }
    }
}
