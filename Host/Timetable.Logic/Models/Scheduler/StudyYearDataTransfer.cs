using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class StudyYearDataTransfer : BaseDataTransfer
    {
        
        public int StartYear { get; set; }
        
        public int EndYear { get; set; }

        public StudyYearDataTransfer() { }

        public StudyYearDataTransfer(StudyYear studyYear)
        {
            Id = studyYear.Id;
            StartYear = studyYear.StartYear;
            EndYear = studyYear.StartYear + studyYear.Length;
        }
    }
}
