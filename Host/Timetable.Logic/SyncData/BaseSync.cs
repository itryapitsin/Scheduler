using Timetable.Data.Context.Interfaces;
using Timetable.Data.IIAS.Context;

namespace Timetable.Sync.Logic.SyncData
{
    public abstract class BaseSync
    {
        public IIIASContext IIASContext;

        public ISchedulerDatabase SchedulerDatabase;
        public abstract void Sync();
    }
}
