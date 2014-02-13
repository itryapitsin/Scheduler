using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class GroupDataTransfer : BaseDataTransfer 
    {
        public string Code { get; set; }
        public int? StudentsCount { get; set; }
        public int ParentId { get; set; }

        //TODO: возможно потом убрать поле
        public string SpecialityName { get; set; }
        
        public GroupDataTransfer(Group group)
        {
            Id = group.Id;
            Code = group.Code;

            if(group.Parent != null)
                ParentId = group.Parent.Id;

            SpecialityName = group.SpecialityName;
        }
    }
}
