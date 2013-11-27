using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class AuditoriumTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AuditoriumTypeViewModel(AuditoriumTypeDataTransfer auditoriumType)
        {
            Id = auditoriumType.Id;
            Name = auditoriumType.Name;
        }
    }
}