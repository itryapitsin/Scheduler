using Timetable.Site.NewDataService;

namespace Timetable.Site.Models.Tutorials
{
    public class TutorialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public TutorialViewModel(TutorialDataTransfer tutorial)
        {
            Id = tutorial.Id;
            Name = tutorial.Name;
            ShortName = tutorial.ShortName;
        }
    }
}