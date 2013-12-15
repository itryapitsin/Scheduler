

using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class WeekTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public WeekTypeViewModel(WeekTypeDataTransfer weekType)
        {
            Id = weekType.Id;
            Name = weekType.Name;
        }
    }
}