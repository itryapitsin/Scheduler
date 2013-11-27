using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public GroupViewModel(GroupDataTransfer @group)
        {
            Id = @group.Id;
            Code = @group.Code;
        }
    }
}