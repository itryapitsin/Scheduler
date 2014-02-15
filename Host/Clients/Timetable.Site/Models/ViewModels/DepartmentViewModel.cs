using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public DepartmentViewModel(DepartmentDataTransfer department)
        {
            Id = department.Id;
            Name = department.Name;
            ShortName = department.ShortName;
        }
    }
}