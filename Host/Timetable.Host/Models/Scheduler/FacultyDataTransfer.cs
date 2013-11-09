using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class FacultyDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public int BranchId { get; set; }
        public FacultyDataTransfer()
        {
        }

        public FacultyDataTransfer(Faculty faculty)
        {
            Id = faculty.Id;
            Name = faculty.Name;
            ShortName = faculty.ShortName;
        }
    }
}
