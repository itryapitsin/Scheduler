using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class AuditoriumTypeDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }
        public string Pattern { get; set; }

        public AuditoriumTypeDataTransfer(AuditoriumType auditoriumType)
        {
            Id = auditoriumType.Id;
            Name = auditoriumType.Name;
            Pattern = auditoriumType.Pattern;
        }
    }
}
