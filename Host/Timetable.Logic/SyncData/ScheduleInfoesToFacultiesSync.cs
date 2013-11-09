using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoesToFacultiesSync: BaseSync
    {
        private DbConnection _connection;

        public ScheduleInfoesToFacultiesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    CES_ID AS Id, 
                    NAME_FACULT
                FROM            
                    SDMS.V_UPL_RASP
                WHERE        
                    (HOURS_WEEK IS NOT NULL) 
                    AND (PCARD_ID IS NOT NULL)
                    AND (EW_ID IS NOT NULL)
                    AND (DIS_CODE IS NOT NULL)
                    AND (UBU_ID IS NOT NULL) 
                    AND (GR_UBU_ID > 0) 
                    AND (GR_UBU_ID IS NOT NULL)
                    AND (KURS_CODE > 0) 
                    AND (KURS_CODE IS NOT NULL) 
                    AND (SPEC_CODE IS NOT NULL) 
                    AND (FACULT_CODE IS NOT NULL)";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.ScheduleInfoes.Include("Faculties").ToList();

            while (reader.Read())
            {
                var scheduleInfoId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                var facultyId = reader.GetInt64(0);
                var faculty = SchedulerDatabase.Faculties.First(x => x.IIASKey == facultyId);
                if (schedulerEntity != null && faculty != null && !schedulerEntity.Faculties.Contains(faculty))
                {
                    schedulerEntity.Faculties.Add(faculty);
                    SchedulerDatabase.Update(schedulerEntity);
                }
            }

            _connection.Close();
        }
    }
}
