using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class FacultyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FacultyViewModel(FacultyDataTransfer faculty)
        {
            Id = faculty.Id;
            Name = faculty.Name;
        }
    }
}