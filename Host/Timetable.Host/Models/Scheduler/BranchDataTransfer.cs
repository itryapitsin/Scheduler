using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class BranchDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }
        public BranchDataTransfer()
        {
        }

        public BranchDataTransfer(Branch branch)
        {
            Id = branch.Id;
            Name = branch.Name;
        }
    }
}
