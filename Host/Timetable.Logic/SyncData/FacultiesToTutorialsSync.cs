using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class FacultiesToTutorialsSync: BaseSync
    {
        private DbConnection _connection;

        public FacultiesToTutorialsSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                    SELECT DISTINCT 
                        DIS_CODE AS Id, 
                        NAME_FACULT AS FacultyName
                    FROM            
                        SDMS.V_UPL_RASP";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.Faculties.Include("Tutorials").ToList();

            while (reader.Read())
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.Name == reader.GetString(1));
                var tutorialId = reader.GetInt64(0);
                var tutorial = SchedulerDatabase.Tutorials.First(x => x.IIASKey == tutorialId);
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
