using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
    public class Auditorium : BaseIIASEntity
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public string Info { get; set; }
        public ICollection<TutorialType> TutorialApplicabilities { get; set; }
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
        public virtual Building Building { get; set; }
        public int BuildingId { get; set; }
        public virtual AuditoriumType AuditoriumType { get; set; }
        public int AuditoriumTypeId { get; set; }
        public Auditorium()
        {
            TutorialApplicabilities = new HashSet<TutorialType>();
            ScheduleInfoes = new HashSet<ScheduleInfo>();
        }
    }
}
