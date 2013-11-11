using System.ServiceModel;
using Timetable.Data.Models;
using Timetable.Host.Models.Scheduler;

namespace Timetable.Host.Interfaces
{
    [ServiceContract]
    public interface IBaseService
    {
        [OperationContract]
        OperationResult Add(BaseEntity dto);

        [OperationContract]
        OperationResult Update(BaseEntity dto);

        [OperationContract]
        OperationResult Delete(BaseEntity dto);
    }
}
