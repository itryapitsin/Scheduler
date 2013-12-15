using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class SemesterViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SemesterViewModel(SemesterDataTransfer semester)
        {
            Id = semester.Id;
            Name = semester.Name;
        }
    }
}