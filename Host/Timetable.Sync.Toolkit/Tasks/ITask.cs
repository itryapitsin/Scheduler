namespace Timetable.Sync.Toolkit.Tasks
{
    public interface ITask
    {
        string Name { get; }

        void Execute();
    }
}
