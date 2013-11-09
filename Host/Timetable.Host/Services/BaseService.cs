using System;
using Timetable.Data.Context;
using Timetable.Data.Models;
using Timetable.Host.Interfaces;

namespace Timetable.Host.Services
{
    public class BaseService: IBaseService
    {
        public SchedulerContext Database { get; set; }

        protected BaseService()
        {
            Database = new SchedulerContext();
        }

        protected OperationResult Crud<T>(Action<T> crud, T entity) where T: BaseEntity
        {
            var result = new OperationResult();

            try
            {
                crud(entity);

                result.Object = entity;
                result.Status = Status.Success;
            }
            catch
            {
                result.Object = entity;
                result.Status = Status.Fail;
            }

            return result;
        }


        public OperationResult Add(BaseEntity entity)
        {
            return Crud(e => Database.Add(e), entity);
        }

        public OperationResult Update(BaseEntity entity)
        {
            return Crud(e => Database.Update(e), entity);
        }

        public OperationResult Delete(BaseEntity entity)
        {
            return Crud(e => Database.Delete(e), entity);
        }
    }
}