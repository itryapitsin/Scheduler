using Timetable.Logic.Models.Scheduler;
namespace Timetable.Site.Models.ViewModels
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PositionViewModel(PositionDataTransfer position)
        {
            Id = position.Id;
            Name = position.Name;
        }
    }
}