using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class ScheduleInfoesToSpecialitiesSync: BaseSync
    {
        private DbConnection _connection;
        private string _commandPattern = @"
                INSERT INTO [dbo].[ScheduleInfoesToSpecialities]
                    ([ScheduleInfo_Id]
                    ,[Speciality_Id])
                VALUES
                    ({0}
                    ,{1});";
        public ScheduleInfoesToSpecialitiesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            //TODO: Нужно будет убрать часть ограничений в WHERE
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    CES_ID,
                    V_STUD_GR.SPEC_BUN_ID 
                FROM            
                    SDMS.V_UPL_RASP, SDMS.V_STUD_GR
                WHERE        
                    (SDMS.V_UPL_RASP.HOURS_WEEK IS NOT NULL) 
                    AND (V_STUD_GR.UBU_ID = SDMS.V_UPL_RASP.GR_UBU_ID)
                    AND (SDMS.V_UPL_RASP.PCARD_ID IS NOT NULL) 
                    AND (SDMS.V_UPL_RASP.EW_ID IS NOT NULL) 
                    AND (SDMS.V_UPL_RASP.DIS_CODE IS NOT NULL)
                    AND (SDMS.V_STUD_GR.UBU_ID IS NOT NULL) 
                    AND (SDMS.V_UPL_RASP.GR_UBU_ID > 0) 
                    AND (SDMS.V_UPL_RASP.UCH_GOG LIKE '2013/%')
                    AND (SDMS.V_UPL_RASP.GR_UBU_ID IS NOT NULL) 
                    AND (SDMS.V_UPL_RASP.KURS_CODE > 0) 
                    AND (SDMS.V_UPL_RASP.KURS_CODE IS NOT NULL) 
                    AND (SDMS.V_UPL_RASP.SPEC_CODE IS NOT NULL) 
                    AND (SDMS.V_UPL_RASP.FACULT_CODE IS NOT NULL)";

            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.ScheduleInfoes.Include("Specialities").ToList();
            var specialities = SchedulerDatabase.Specialities.ToList();
            var command = String.Empty;

            while (reader.Read())
            {
                var scheduleInfoId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == scheduleInfoId);
                var id = reader.GetInt64(1);
                var speciality = specialities.FirstOrDefault(x => x.Id == id);
                if (schedulerEntity != null && speciality != null && !schedulerEntity.Specialities.Contains(speciality))
                {
                    schedulerEntity.Specialities.Add(speciality);
                    //SchedulerDatabase.Update(schedulerEntity);
                    command += String.Format(_commandPattern, schedulerEntity.Id, speciality.Id);
                }
            }

            if(!String.IsNullOrEmpty(command))
                SchedulerDatabase.RawSqlCommand(command);

            //TODO: Добавить загрузку связей scheduleInfoes to specialities из таблицы v_rasp_desk_n

            _connection.Close();
        }
    }
}
