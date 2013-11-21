using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Models.Scheduler;


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
            if(auditorium.Capacity.HasValue)
                Capacity = auditorium.Capacity.Value;
            Name = auditorium.Name;
            Info = auditorium.Info;
        }
    }
}