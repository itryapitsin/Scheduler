using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class DepartmentDataTransfer : BaseDataTransfer 
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        public DepartmentDataTransfer()
        {
        }

        public DepartmentDataTransfer(Department department)
        {
            Id = department.Id;
            Name = department.Name;
            ShortName = department.ShortName;
        }
    }
}
