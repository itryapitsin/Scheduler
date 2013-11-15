using System.Runtime.Serialization;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Base.Entities.Personalization
{
    [DataContract]
    public class UserRole: BaseIIASEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public UserRoleTypes Type { get; set; }
    }
}
