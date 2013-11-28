using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Context.Interfaces
{
    public interface IDatabase
    {
        void Add<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseIIASEntity;

        void Update<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseIIASEntity;

        void Delete<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseIIASEntity;

        void SaveChanges();
    }
}
