using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string SpecialityName { get; set; }

        public GroupViewModel(GroupDataTransfer @group)
        {
            Id = @group.Id;
            Code = @group.Code;
            SpecialityName = @group.SpecialityName;
        }
    }
}