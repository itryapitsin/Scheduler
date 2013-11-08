using System;
using Ninject;
using Timetable.Base.Entities;
using Timetable.Data.Context.Interfaces;
using Timetable.Data.Models;
using Timetable.Host.Interfaces;

namespace Timetable.Host.Services
{
    public class BaseService<TDatabase>: IBaseService where TDatabase: IDatabase
    {
        [Inject]
        public TDatabase Database { get; set; }

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