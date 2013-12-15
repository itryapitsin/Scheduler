using System;

namespace Timetable.Dispatcher.Tasks
{
    public interface ITask
    {
        DateTime CreateDate { get; set; }

        string Id { get; set; }

        void Execute();
    }
}
