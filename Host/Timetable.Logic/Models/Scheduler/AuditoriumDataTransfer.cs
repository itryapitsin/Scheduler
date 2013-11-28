using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class AuditoriumDataTransfer : BaseDataTransfer
    {
        
        public string Number { get; set; }
        
        public string Name { get; set; }
        
        public int? Capacity { get; set; }
        
        public string Info { get; set; }
        
        public BuildingDataTransfer Building { get; set; }
        
        public AuditoriumTypeDataTransfer AuditoriumType { get; set; }
        public AuditoriumDataTransfer() {}

        public AuditoriumDataTransfer(Auditorium auditorium)
        {
            Id = auditorium.Id;
            Number = auditorium.Number;
            Name = auditorium.Name;
            Capacity = auditorium.Capacity;
            Info = auditorium.Info;
            Building = new BuildingDataTransfer(auditorium.Building);
            AuditoriumType = new AuditoriumTypeDataTransfer(auditorium.AuditoriumType);
        }
    }
}
