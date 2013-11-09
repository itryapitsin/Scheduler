using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class GroupDataTransfer : BaseDataTransfer 
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public int SpecialityId { get; set; }
        [DataMember]
        public int? StudentsCount { get; set; }
        [DataMember]
        public int ParentId { get; set; }
        
        public GroupDataTransfer(Group group)
        {
            Id = group.Id;
            Code = group.Code;
            CourseId = group.Course.Id;
            SpecialityId = group.Speciality.Id;

            if(group.Parent != null)
                ParentId = group.Parent.Id;
        }
    }
}
