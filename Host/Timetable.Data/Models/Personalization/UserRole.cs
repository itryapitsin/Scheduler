using System.Runtime.Serialization;
using Timetable.Data.Models;

namespace Timetable.Base.Entities.Personalization
{
    [DataContract]
    public class UserRole: BaseEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public UserRoleTypes Type { get; set; }
    }
}
