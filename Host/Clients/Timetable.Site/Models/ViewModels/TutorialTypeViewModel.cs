using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class TutorialTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TutorialTypeViewModel(TutorialTypeDataTransfer tutorialType)
        {
            Id = tutorialType.Id;
            Name = tutorialType.Name;
        }
    }
}