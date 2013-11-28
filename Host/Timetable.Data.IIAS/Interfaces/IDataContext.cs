using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Timetable.Data.IIAS.Interfaces
{
    public interface IDataContext
    {
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        IQueryable<TEntity> RawSqlQuery<TEntity>(
            string query, 
            params object[] parameters) where TEntity : class;

        int RawSqlCommand(
            string command,
            params object[] parameters);

        IQueryable<dynamic> RawSqlQuery(
            List<Type> types,
            List<string> names,
            string query,
            params object[] parameters);
    }
}
