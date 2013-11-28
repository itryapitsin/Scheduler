using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    public class SemesterDataTransfer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SemesterDataTransfer(Semester semester)
        {
            Id = semester.Id;
            Name = semester.Name;
        }
    }
}
