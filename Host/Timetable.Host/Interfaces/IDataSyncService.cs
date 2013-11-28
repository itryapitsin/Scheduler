using System.ServiceModel;

namespace Timetable.Host.Interfaces
{
    [ServiceContract]
    public interface IDataSyncService
    {
        [OperationContract]
        void DoSync();

        [OperationContract]
        int GetProgress();
    }
}
