using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class TutorialTypeDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }

        public TutorialTypeDataTransfer()
        {
        }

        public TutorialTypeDataTransfer(TutorialType tutorialType)
        {
            Id = tutorialType.Id;
            Name = tutorialType.Name;
        }
    }
}
