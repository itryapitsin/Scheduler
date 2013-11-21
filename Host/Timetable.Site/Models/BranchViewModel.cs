using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class BranchViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BranchViewModel(BranchDataTransfer branch)
        {
            Id = branch.Id;
            Name = branch.Name;
        }
    }
}