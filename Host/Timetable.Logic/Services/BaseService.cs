using System;
using Timetable.Data.Context;
using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Interfaces;

namespace Timetable.Logic.Services
{
    public class BaseService : IBaseService
    {
        public SchedulerContext Database { get; set; }

        protected BaseService()
        {
            Database = new SchedulerContext();
        }

        public void Add(BaseIIASEntity dto)
        {
            Database.Add(dto);
        }

        public void Update(BaseIIASEntity dto)
        {
            Database.Update(dto);
        }

        public void Delete(BaseIIASEntity dto)
        {
            dto.IsActual = false;
            Database.Update(dto);
        }
    }
}