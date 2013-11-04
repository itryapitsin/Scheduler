using Timetable.Base.Entities;

namespace Timetable.Base.Interfaces.DataSources
{
    public interface IDatabase
    {
        void Add<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseEntity;

        void Update<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseEntity;

        void Delete<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseEntity;

        void SaveChanges();
    }
}
