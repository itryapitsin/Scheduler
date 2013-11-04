using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Base.Entities.Scheduler
{
    [DataContract(IsReference = true)]
    public class Department: BaseEntity 
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "ShortName")]
        public string ShortName { get; set; }

        [DataMember(Name = "Faculties")]
        public ICollection<Faculty> Faculties { get; set; }

        [DataMember(Name = "Lecturers")]
        public ICollection<Lecturer> Lecturers { get; set; }

        public Department()
        {
            Lecturers = new HashSet<Lecturer>();
            Faculties = new Collection<Faculty>();
        }
    }
}
