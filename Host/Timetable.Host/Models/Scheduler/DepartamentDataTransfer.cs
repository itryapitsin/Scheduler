using System.Collections.Generic;
using System.Linq;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class DepartmentDataTransfer : BaseDataTransfer 
    {
        public string Name { get; set; }
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
