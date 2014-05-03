using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class AuditoriumTypeDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        
        public string Pattern { get; set; }

        public AuditoriumTypeDataTransfer(AuditoriumType auditoriumType)
        {
            if (auditoriumType != null)
            {
                Name = auditoriumType.Name;
                Pattern = auditoriumType.Pattern;
                Id = auditoriumType.Id;
            }
        }

        public AuditoriumTypeDataTransfer()
        {
        }
    }
}
