using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Interfaces
{
    public interface IBaseService
    {
        void Add(BaseIIASEntity dto);

        void Update(BaseIIASEntity dto);

        void Delete(BaseIIASEntity dto);
    }
}
