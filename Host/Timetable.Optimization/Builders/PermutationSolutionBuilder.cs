using Metaheuristics.Providers.CombineProvider;
using Metaheuristics.Providers.CostProvider;
using Metaheuristics.Providers.NeighborhoodProvider;
using Metaheuristics.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;
using Timetable.Optimization.Providers;

namespace Timetable.Optimization.Builders
{
    public class PermutationSolutionBuilder : ISolutionBuilder<ScheduleInfo>
    {
        private int _n;
        private ScheduleInfo[] _items;
        private readonly ICostProvider<ScheduleInfo> _costProvider;
        private readonly INeighborhoodProvider<ScheduleInfo> _neighborhoodProvider;
        private readonly ICombineProvider<ScheduleInfo> _combineProvider;

        public PermutationSolutionBuilder(
            int permutationLength, 
            ScheduleInfo[] items,
            ICostProvider<ScheduleInfo> costProvider,
            INeighborhoodProvider<ScheduleInfo> neighborhoodProvider,
            ICombineProvider<ScheduleInfo> combineProvider)
        {
            _n = permutationLength;
            _items = items;
            _costProvider = costProvider;
            _neighborhoodProvider = neighborhoodProvider;
            _combineProvider = combineProvider;
        }

        public ISolution<ScheduleInfo> BuildSolution()
        {
            // Fill with random data is needed
            var result = new PermutationSolution(_n, _costProvider, _neighborhoodProvider, _combineProvider);
            for (int i = 0; i <= result.N; i++)
                result.Elements.Add(_items[i]);
            return result;
        }
    }
}
