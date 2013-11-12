using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class AuditoriumTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AuditoriumTypeViewModel(AuditoriumType auditoriumType)
        {
            Id = auditoriumType.Id;
            Name = auditoriumType.Name;
        }

        public AuditoriumTypeViewModel(AuditoriumTypeDataTransfer auditoriumType)
        {
            Id = auditoriumType.Id;
            Name = auditoriumType.Name;
        }
    }
}