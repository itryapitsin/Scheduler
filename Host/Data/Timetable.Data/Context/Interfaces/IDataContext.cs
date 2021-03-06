﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Context.Interfaces
{
    public interface IDataContext
    {
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseIIASEntity;

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
