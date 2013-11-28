using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class GroupDataTransfer : BaseDataTransfer 
    {
        
        public string Code { get; set; }
        
        public int CourseId { get; set; }
        
        public int SpecialityId { get; set; }
        
        public int? StudentsCount { get; set; }
        
        public int ParentId { get; set; }
        
        public GroupDataTransfer(Group group)
        {
            Id = group.Id;
            Code = group.Code;
            CourseId = group.CourseId;
            SpecialityId = group.SpecialityId;

            if(group.Parent != null)
                ParentId = group.Parent.Id;
        }
    }
}
