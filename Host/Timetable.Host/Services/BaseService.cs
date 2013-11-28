using System;
using Timetable.Data.Context;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;
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

        protected OperationResult Crud<T>(Action<T> crud, T entity) where T: BaseIIASEntity
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


        public OperationResult Add(BaseIIASEntity dto)
        {
            return Crud(e => Database.Add(e), dto);
        }

        public OperationResult Update(BaseIIASEntity dto)
        {
            return Crud(e => Database.Update(e), dto);
        }

        public OperationResult Delete(BaseIIASEntity dto)
        {
            return Crud(e =>
            {
                e.IsActual = false;
                Database.Update(e);
            }, dto);
        }
    }
}