using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
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