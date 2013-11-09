using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class DepartmentsToFacultiesSync: BaseSync
    {
        private DbConnection _connection;

        public DepartmentsToFacultiesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT 
                                     SDMS.O_USE_BASE_UNITS.UBU_ID AS DepartmentId, 
                                     SDMS.V_UPL_RASP.NAME_FACULT AS FacultyName, 
                                     SDMS.O_USE_BASE_UNITS.NAME_SHORT
                                FROM            
                                     SDMS.V_UPL_RASP, SDMS.O_USE_BASE_UNITS
                                WHERE
                                     SDMS.V_UPL_RASP.UBU_ID = SDMS.O_USE_BASE_UNITS.UBU_ID
                                ORDER BY DepartmentId";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.Faculties.ToList();

            while (reader.Read())
            {
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.Name == reader.GetString(1));
                var depId = reader.GetInt64(0);
                var dep = SchedulerDatabase.Departments.First(x => x.IIASKey == depId);
                if (schedulerEntity != null)
                {
                    schedulerEntity.Departments.Add(dep);
                    SchedulerDatabase.Update(schedulerEntity);
                }
            }

            _connection.Close();
        }
    }
}
