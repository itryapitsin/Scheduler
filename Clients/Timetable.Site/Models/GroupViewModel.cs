using Timetable.Site.NewDataService;

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