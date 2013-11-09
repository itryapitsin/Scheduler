using System.Collections.Generic;
using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class AuditoriumDataTransfer : BaseDataTransfer
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public string Info { get; set; }
        public int BuildingId { get; set; }
        public int AuditoriumTypeId { get; set; }
        public AuditoriumDataTransfer()
        {
        }

        public AuditoriumDataTransfer(Auditorium auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            Name = auditorium.Name;
            Capacity = auditorium.Capacity;
            Info = auditorium.Info;
            BuildingId = auditorium.Building.Id;
            AuditoriumTypeId = auditorium.AuditoriumType.Id;
        }
    }
}
