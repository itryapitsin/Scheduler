using Metaheuristics.Providers.CombineProvider;
using Metaheuristics.Providers.NeighborhoodProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Base.Entities.Scheduler;
using Timetable.Data.Context;
using Timetable.Optimization.Builders;
using Timetable.Optimization.Providers;

namespace Timetable.Optimization.Workbench
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new SchedulerContext();

            var neigborhoodProvider = new OneSwapNeighborhoodProvider<ScheduleInfo>();
            var combineProvider = new OneSplitCombineProvider<ScheduleInfo>();
            var costProvider = new CostProvider(database);

            //var providerContainer = new ProviderContainer(costProvider, neigborhoodProvider, combineProvider);

            //var permutationSolutionBuilder = new PermutationSolutionBuilder(
            //var localOptimizationSolver = new LocalOptimizationSolver(permutationSolutionBuilder);
        }
    }
}
