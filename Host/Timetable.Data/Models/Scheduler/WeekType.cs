using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    [DataContract(IsReference = true)]
    public class WeekType: BaseEntity
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
    }
}
