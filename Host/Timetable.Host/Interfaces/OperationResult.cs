using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Interfaces
{
    public class OperationResult
    {
        public Status Status { get; set; }

        public BaseIIASEntity Object { get; set; }
    }

    public enum Status
    {
        Success,
        Fail
    }
}