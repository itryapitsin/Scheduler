

using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DepartmentViewModel(Department t)
        {
            Id = t.Id;
            Name = t.Name;
            Name = t.ShortName;
        }

        public DepartmentViewModel(DepartmentDataTransfer t)
        {
            Id = t.Id;
            Name = t.Name;
            Name = t.ShortName;
        }
    }
}