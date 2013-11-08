using Timetable.Base.Entities;
using Timetable.Data.Models;

namespace Timetable.Data.Context.Interfaces
{
    public interface IDatabase
    {
        void Add<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseEntity;

        void Update<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseEntity;

        void Delete<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : BaseEntity;

        void SaveChanges();
    }
}
