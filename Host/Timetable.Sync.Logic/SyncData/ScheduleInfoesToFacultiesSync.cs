using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoesToFacultiesSync : BaseSync
    {
        private DbConnection _connection;

        private string _commandPattern = "INSERT INTO [dbo].[ScheduleInfoesToFaculties]([ScheduleInfo_Id],[Faculty_Id])VALUES({0},{1});";

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
            var faculties = SchedulerDatabase.Faculties.ToList();
            var command = String.Empty;
            try
            {
                while (reader.Read())
                {
                    var scheduleInfoId = reader.GetInt64(0);
                    var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                    var facultyId = reader.GetString(1);
                    var faculty = faculties.FirstOrDefault(x => x.Name == facultyId);
                    if (schedulerEntity != null && faculty != null && !schedulerEntity.Faculties.Contains(faculty))
                    {
                        command += String.Format(_commandPattern, schedulerEntity.Id, faculty.Id);
                    }
                }
                if (!String.IsNullOrEmpty(command))
                    SchedulerDatabase.RawSqlCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            _connection.Close();
        }
    }
}
