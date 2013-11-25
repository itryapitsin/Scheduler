using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class StudyYearViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public StudyYearViewModel(StudyYearDataTransfer studyYear)
        {
            Id = studyYear.Id;
            Name = string.Format("{0}/{1}", studyYear.StartYear, studyYear.EndYear);
        }
    }
}