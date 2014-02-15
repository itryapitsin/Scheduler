using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class AuditoriumViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string AuditoriumTypeName { get; set; }
        public int AuditoriumTypeId { get; set; }

        public string BuildingName { get; set; }
        public int BuildingId { get; set; }

        public AuditoriumViewModel(AuditoriumDataTransfer auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            if(auditorium.Capacity.HasValue)
                Capacity = auditorium.Capacity.Value;
            Name = auditorium.Name;
            Info = auditorium.Info;
            if (auditorium.AuditoriumType != null)
            {
                AuditoriumTypeName = auditorium.AuditoriumType.Name;
                AuditoriumTypeId = auditorium.AuditoriumType.Id;
            }
            if (auditorium.Building != null)
            {
                BuildingName = auditorium.Building.Name;
                BuildingId = auditorium.Building.Id;
            }
        }
    }
}