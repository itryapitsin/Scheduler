using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Base.Interfaces.DataSources;
using Timetable.Data.IIAS.Context;

namespace Timetable.Logic.SyncData
{
    public abstract class BaseSync
    {
        public IIIASContext IIASContext;

        public ISchedulerDatabase SchedulerDatabase;
        public abstract void Sync();
    }
}
