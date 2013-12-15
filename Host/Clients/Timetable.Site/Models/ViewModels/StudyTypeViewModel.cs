using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class StudyTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public StudyTypeViewModel(StudyTypeDataTransfer studyType)
        {
            Id = studyType.Id;
            Name = studyType.Name;
        }
    }
}