using System;
using System.Data.Common;
using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Sync.Logic.SyncData
{
    public class WeekTypeSync: BaseSync
    {
        private DbConnection _connection;

        public WeekTypeSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override async void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT 
                        PERIOD as Name
                    FROM
                        SDMS.U_RASP_STR
                    WHERE
                        (STATYS = 'Y') AND (PERIOD IS NOT NULL)";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.WeekTypes.ToList();

            while (reader.Read())
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.Name == reader.GetString(0));
                if (schedulerEntity == null)
                {
                    schedulerEntity = new WeekType
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Name = reader.GetString(0),
                        IsActual = true
                    };
                    SchedulerDatabase.Add(schedulerEntity);
                }
            }

            _connection.Close();
        }
    }
}
