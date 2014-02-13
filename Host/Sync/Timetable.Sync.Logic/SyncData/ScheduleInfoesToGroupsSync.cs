using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoesToGroupsSync : BaseSync
    {
        private DbConnection _connection;
        private string _commandPattern = "INSERT INTO [dbo].[ScheduleInfoesToGroups]([ScheduleInfo_Id],[Group_Id])VALUES({0},{1});";

        public ScheduleInfoesToGroupsSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            //Insert from V_UPL_RASP
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    CES_ID, 
                    GR_UBU_ID
                FROM            
                    SDMS.V_UPL_RASP
                WHERE
                    (HOURS_WEEK IS NOT NULL) 
                        AND (PCARD_ID IS NOT NULL) 
                        AND (EW_ID IS NOT NULL) 
                        AND (DIS_CODE IS NOT NULL) 
                        AND (UCH_GOG LIKE '2013/%')
                        AND (KURS_CODE > 0) 
                        AND (KURS_CODE IS NOT NULL) 
                        AND (SPEC_CODE IS NOT NULL) 
                        AND (FACULT_CODE IS NOT NULL)
                        AND (GR_UBU_ID > 0) 
                        AND (GR_UBU_ID IS NOT NULL)";

            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.ScheduleInfoes.Include("Groups").ToList();
            var groups = SchedulerDatabase.Groups.ToList();
            var command = String.Empty;

            while (reader.Read())
            {
                var scheduleInfoId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                var id = reader.GetInt64(1);
                var @group = groups.FirstOrDefault(x => x.IIASKey == id);
                if (schedulerEntity != null && @group != null && !schedulerEntity.Groups.Contains(@group))
                {
                    schedulerEntity.Groups.Add(@group);
                    //SchedulerDatabase.Update(schedulerEntity);
                    command += String.Format(_commandPattern, schedulerEntity.Id, @group.Id);
                }
            }

            if (!String.IsNullOrEmpty(command))
                SchedulerDatabase.RawSqlCommand(command);

            //Insert from V_RASP_DESK_N
            DbCommand cmd2 = _connection.CreateCommand();
            cmd2.CommandText = @"
                SELECT DISTINCT 
                    CES_ID, 
                    UBU_UBU_ID
                FROM            
                    SDMS.V_UPL_RASP, SDMS.V_RASP_DESK_N
                WHERE
			        CELS_CES_ID = CES_ID 
			    AND 
                   	((GR_UBU_ID = 0) OR (GR_UBU_ID IS NULL));";

            reader = cmd2.ExecuteReader();
            schedulerEntities = SchedulerDatabase.ScheduleInfoes.Include("Groups").ToList();
            groups = SchedulerDatabase.Groups.ToList();
            command = String.Empty;

            while (reader.Read())
            {
                var scheduleInfoId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                var id = reader.GetInt64(1);
                var @group = groups.FirstOrDefault(x => x.IIASKey == id);
                if (schedulerEntity != null && @group != null && !schedulerEntity.Groups.Contains(@group))
                {
                    schedulerEntity.Groups.Add(@group);
                    //SchedulerDatabase.Update(schedulerEntity);
                    command += String.Format(_commandPattern, schedulerEntity.Id, @group.Id);
                }
            }

            if (!String.IsNullOrEmpty(command))
                SchedulerDatabase.RawSqlCommand(command);

            _connection.Close();
        }
    }
}
