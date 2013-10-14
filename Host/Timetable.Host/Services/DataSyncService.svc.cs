using System;
using Timetable.Host.Interfaces;

namespace Timetable.Host.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataSyncService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataSyncService.svc or DataSyncService.svc.cs at the Solution Explorer and start debugging.
    public class DataSyncService : IDataSyncService
    {
        public void DoSync()
        {
        }

        public int GetProgress()
        {
            throw new NotImplementedException();
        }
    }
}
