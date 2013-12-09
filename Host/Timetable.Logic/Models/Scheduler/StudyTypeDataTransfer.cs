using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class StudyTypeDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }
        public StudyTypeDataTransfer() { }

        public StudyTypeDataTransfer(StudyType studyType)
        {
            Id = studyType.Id;
            Name = studyType.Name;
        }
    }
}
