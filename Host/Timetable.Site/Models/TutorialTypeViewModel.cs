

using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class TutorialTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TutorialTypeViewModel(TutorialType tutorialType)
        {
            Id = tutorialType.Id;
            Name = tutorialType.Name;
        }

        public TutorialTypeViewModel(TutorialTypeDataTransfer tutorialType)
        {
            Id = tutorialType.Id;
            Name = tutorialType.Name;
        }
    }
}