using Metaheuristics.Providers.CombineProvider;
using Metaheuristics.Providers.NeighborhoodProvider;
using Timetable.Data.Context;
using Timetable.Data.Models.Scheduler;
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
