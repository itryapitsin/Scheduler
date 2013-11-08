using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    [DataContract(IsReference = true)]
    public class Position: BaseEntity
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Lecturers")]
        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}
