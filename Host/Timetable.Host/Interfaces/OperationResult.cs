using Timetable.Data.Models;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Interfaces
{
    public class OperationResult
    {
        public Status Status { get; set; }

        public BaseEntity Object { get; set; }
    }

    public enum Status
    {
        Success,
        Fail
    }
}