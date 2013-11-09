using System.Collections.Generic;
using System.Collections.ObjectModel;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class GroupDataTransfer : BaseDataTransfer 
    {
        public string Code { get; set; }
        public CourseDataTransfer Course { get; set; }
        public SpecialityDataTransfer Speciality { get; set; }
        public int? StudentsCount { get; set; }
        public GroupDataTransfer Parent { get; set; }

        //[DataMember(Name = "ParentId")]
        //public int? ParentId { get; set; }

        public GroupDataTransfer(Group group)
        {
            Id = group.Id;
            Code = group.Code;
            Course = new CourseDataTransfer(group.Course);
            Speciality = new SpecialityDataTransfer(group.Speciality);

            if(group.Parent != null)
                Parent = new GroupDataTransfer(group.Parent);
        }
    }
}
