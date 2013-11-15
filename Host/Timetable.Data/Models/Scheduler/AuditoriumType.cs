using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class AuditoriumType : BaseIIASEntity
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
        public ICollection<Auditorium> Auditoriums { get; set; }

        public AuditoriumType()
        {
            Auditoriums = new Collection<Auditorium>();
        }
    }
}
