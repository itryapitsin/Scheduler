using System;

namespace Timetable.Sync.Toolkit.Tasks
{
    public interface ITask
    {
        DateTime CreateDate { get; set; }

        string Id { get; set; }

        void Execute();
    }
}
