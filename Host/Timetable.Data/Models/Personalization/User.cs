using System;
using System.Runtime.Serialization;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Base.Entities.Personalization
{
    [DataContract]
    public class User: BaseIIASEntity
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public UserRole Role { get; set; }

        [DataMember]
        public int RoleId { get; set; }
    }
}
