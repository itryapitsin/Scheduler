using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Base.Entities.Scheduler;

namespace Timetable.Logic.SyncData
{
    public class StudyYearSync : BaseSync
    {
        private DbConnection _connection;

        public StudyYearSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override async void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                    SELECT DISTINCT UCH_GOG
                    FROM SDMS.V_UPL_RASP
                    ORDER BY UCH_GOG";
            var reader = cmd.ExecuteReader();

            var schedulerEntities = SchedulerDatabase.StudyYears.ToList();

            while (reader.Read())
            {
                try
                {
                    var content = reader.GetString(0);
                    if (String.IsNullOrEmpty(content))
                        continue;

                    var years = content.Split(new[] { " - ", " / ", "/", "-" }, StringSplitOptions.RemoveEmptyEntries);
                    var startYear = int.Parse(years[0]);
                    var length = int.Parse(years[1]) - startYear;
                    var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.StartYear == startYear && x.Length == length);
                    if (schedulerEntity == null)
                    {
                        schedulerEntity = new StudyYear
                        {
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            StartYear = startYear,
                            Length = length,
                            IsActual = true
                        };
                        SchedulerDatabase.Add(schedulerEntity);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            _connection.Close();
        }
    }
}
