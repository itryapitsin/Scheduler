using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class PositionDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        public PositionDataTransfer()
        {
        }

        public PositionDataTransfer(Position position)
        {
            Id = position.Id;
            Name = position.Name;
        }
    }
}
