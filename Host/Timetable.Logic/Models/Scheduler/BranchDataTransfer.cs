using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    public class BranchDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }
        public BranchDataTransfer() {}

        public BranchDataTransfer(Branch branch)
        {
            Id = branch.Id;
            Name = branch.ShortName ?? branch.Name;
        }
    }
}
