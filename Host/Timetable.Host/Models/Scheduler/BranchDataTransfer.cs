using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class BranchDataTransfer : BaseDataTransfer
    {
        [DataMember]
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
