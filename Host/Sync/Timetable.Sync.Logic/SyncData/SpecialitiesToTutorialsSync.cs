using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class SpecialitiesToTutorialsSync: BaseSync
    {
        private DbConnection _connection;

        public SpecialitiesToTutorialsSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    DIS_CODE AS TutorialId, 
                    SPEC_UBU_ID AS SpecialityId
                FROM            
                    SDMS.V_UPL_RASP";
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var specialityId = reader.GetInt64(1);
                var schedulerEntity = SchedulerDatabase.Specialities.Include("Tutorials").FirstOrDefault(x => x.IIASKey == specialityId);
                var id = reader.GetInt64(0);
                var tutorial = SchedulerDatabase.Tutorials.FirstOrDefault(x => x.IIASKey == id);
                if (schedulerEntity != null && tutorial != null && !schedulerEntity.Tutorials.Contains(tutorial))
                {
                    schedulerEntity.Tutorials.Add(tutorial);
                    SchedulerDatabase.Update(schedulerEntity);
                }
            }

            _connection.Close();
        }
    }
}
