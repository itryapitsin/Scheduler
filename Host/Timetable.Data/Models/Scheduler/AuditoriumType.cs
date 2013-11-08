using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    [DataContract(IsReference = true)]
    public class AuditoriumType : BaseEntity
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        public string Pattern { get; set; }

        [DataMember(Name = "Auditoriums")]
        public virtual ICollection<Auditorium> Auditoriums { get; set; }
    }
}
