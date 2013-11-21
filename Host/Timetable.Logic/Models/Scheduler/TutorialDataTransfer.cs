using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class TutorialDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        
        public string ShortName { get; set; }
        public TutorialDataTransfer()
        {
        }

        public TutorialDataTransfer(Tutorial tutorial)
        {
            Id = tutorial.Id;
            Name = tutorial.Name;
            ShortName = tutorial.ShortName;
        }
    }
}
