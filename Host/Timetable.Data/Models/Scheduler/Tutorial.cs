using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    [DataContract(IsReference = true)]
    public class Tutorial : BaseEntity
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "ShortName")]
        public string ShortName { get; set; }

        [DataMember(Name = "Faculties")]
        public ICollection<Faculty> Faculties { get; set; }

        [DataMember(Name = "Specialities")]
        public ICollection<Speciality> Specialities { get; set; }

        [DataMember(Name = "ScheduleInfoes")]
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }
    }
}
