using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class AuditoriumViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public AuditoriumViewModel(AuditoriumDataTransfer auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            Capacity = (int)auditorium.Capacity;
            Name = auditorium.Name;
            Info = auditorium.Info;
        }

        public AuditoriumViewModel(DataService.Auditorium auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            Capacity = (int)auditorium.Capacity;
            Name = auditorium.Name;
            Info = auditorium.Info;
        }
    }
}