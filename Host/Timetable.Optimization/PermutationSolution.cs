using Metaheuristics.Providers.CombineProvider;
using Metaheuristics.Providers.CostProvider;
using Metaheuristics.Providers.NeighborhoodProvider;
using Metaheuristics.Solutions;
using System;
using System.Collections.Generic;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Optimization
{
	// Permutation solution class
    public class PermutationSolution : ISolution<ScheduleInfo>
    {
        //[Inject]
        private readonly ICostProvider<ScheduleInfo> _costProvider;
        private readonly INeighborhoodProvider<ScheduleInfo> _neighborhoodProvider;
        private readonly ICombineProvider<ScheduleInfo> _combineProvider;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="n">Number of permutation elements</param>
        public PermutationSolution(int n,
            ICostProvider<ScheduleInfo> costProvider,
            INeighborhoodProvider<ScheduleInfo> neighborhoodProvider,
            ICombineProvider<ScheduleInfo> combineProvider)
        {
            Count = n;
            Elements = new List<ScheduleInfo>(Count + 1);

            _costProvider = costProvider;
            _neighborhoodProvider = neighborhoodProvider;
            _combineProvider = combineProvider;
        }

        /// <summary>
        /// Number of permutation elements
        /// </summary>
        public int Count { get; set; }
        
        private double? _cost;
        /// <summary>
        /// Solution cost
        /// </summary>
        public double Cost
        {
            get
            {
                if (!_cost.HasValue)
                    _cost = _costProvider.GetCost(this);
                return _cost.Value;
            }
            set { _cost = value; }
        }

        /// <summary>
        /// Clone current solution
        /// </summary>
        /// <returns>New solution with same items</returns>
        public ISolution<ScheduleInfo> Clone()
        {
            var result = new PermutationSolution(Count, _costProvider, _neighborhoodProvider, _combineProvider);
            for (int i = 0; i <= result.N; i++)
                result.Elements.Add(Elements[i]);
            return result;
        }

        /// <summary>
        /// Get list of solution neighbors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISolution<ScheduleInfo>> GetNeighbors()
        {
            return _neighborhoodProvider.GetNeighbors(this);
        }

        /// <summary>
        /// Get best neighbor solution
        /// </summary>
        /// <returns></returns>
        public ISolution<ScheduleInfo> GetBestNeighbor()
        {
            return _neighborhoodProvider.GetBestNeighbor(this);
        }

        public int N { get; set; }

        /// <summary>
        /// Permutation elements
        /// </summary>
        public List<ScheduleInfo> Elements { get; set; }

        public ISolution<ScheduleInfo> Combine(ISolution<ScheduleInfo> solution)
        {
            return _combineProvider.Combine(this, solution);
        }

        public ISolution<ScheduleInfo> GetRandomNeighbor()
        {
            return _neighborhoodProvider.GetRandomNeighbor(this);
        }

        /// <summary>
        /// Check if solutions are equal
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        public bool IsEqual(ISolution<ScheduleInfo> solution)
        {
            for (int i = 1; i <= this.N; i++)
                if (this.Elements[i] != solution.Elements[i])
                    return false;
            return true;
        }

        public void FillWithRandomData()
        {
            Random rnd = new Random();
            int swaps = rnd.Next(this.Count * this.Count);

            for (int k = 0; k < swaps; k++)
            {
                int i = rnd.Next(this.Count) + 1;
                int j = rnd.Next(this.Count) + 1;

                var t = Elements[i];
                Elements[i] = Elements[j];
                Elements[j] = t;
            }
        }
    }
}
