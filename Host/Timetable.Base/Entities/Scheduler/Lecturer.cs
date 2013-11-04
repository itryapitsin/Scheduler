using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Base.Entities.Scheduler
{
    [DataContract(IsReference = true)]
    public class Lecturer: BaseEntity
    {
        [DataMember(Name = "Lastname")]
	    public string Lastname { get; set; }

        [DataMember(Name = "Firstname")]
	    public string Firstname { get; set; }

        [DataMember(Name = "Middlename")]
	    public string Middlename { get; set; }

        [DataMember(Name = "Contacts")]
	    public string Contacts { get; set; }

        [DataMember(Name = "Departments")]
        public ICollection<Department> Departments { get; set; }

        [DataMember(Name = "Positions")]
        public ICollection<Position> Positions { get; set; }
            
        [DataMember(Name = "ScheduleInfoes")]
        public ICollection<ScheduleInfo> ScheduleInfoes { get; set; }

        public Lecturer()
        {
            ScheduleInfoes = new Collection<ScheduleInfo>();
            Departments = new Collection<Department>();
            Positions = new Collection<Position>();
        }
    }
}
