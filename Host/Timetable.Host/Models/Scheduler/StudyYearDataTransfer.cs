using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class StudyYearDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public int StartYear { get; set; }
        [DataMember]
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
