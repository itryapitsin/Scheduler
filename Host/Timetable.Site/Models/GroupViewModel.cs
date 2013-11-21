

using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public GroupViewModel(Group @group)
        {
            Id = @group.Id;
            Code = @group.Code;
        }

        public GroupViewModel(GroupDataTransfer @group)
        {
            Id = @group.Id;
            Code = @group.Code;
        }
    }
}