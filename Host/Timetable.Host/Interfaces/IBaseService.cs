using System.ServiceModel;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Interfaces
{
    [ServiceContract]
    public interface IBaseService
    {
        [OperationContract]
        OperationResult Add(BaseIIASEntity dto);

        [OperationContract]
        OperationResult Update(BaseIIASEntity dto);

        [OperationContract]
        OperationResult Delete(BaseIIASEntity dto);
    }
}
