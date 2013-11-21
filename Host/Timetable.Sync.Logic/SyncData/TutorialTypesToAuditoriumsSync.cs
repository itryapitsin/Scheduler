using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class TutorialTypesToAuditoriumsSync: BaseSync
    {
        private DbConnection _connection;

        public TutorialTypesToAuditoriumsSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            throw new NotImplementedException();
        }
    }
}
