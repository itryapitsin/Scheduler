using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class BranchesToCoursesSync: BaseSync
    {
        private DbConnection _connection;

        public BranchesToCoursesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                    SELECT DISTINCT 
                        SDMS.V_STUD_GR.KURS_BUN_ID AS Id, 
                        SDMS.V_STUD_GR.UBU_ID_PODR AS BrancheID
                    FROM            
                        SDMS.V_STUD_GR, SDMS.O_BASE_UNIT
                    WHERE        
                        SDMS.V_STUD_GR.KURS_BUN_ID = SDMS.O_BASE_UNIT.BUN_ID";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.Branches.Include("Courses").ToList();

            while (reader.Read())
            {
                var branch = schedulerEntities.FirstOrDefault(x => x.IIASKey == reader.GetInt64(1));
                var courseId = reader.GetInt64(0);
                var course = SchedulerDatabase.Courses.FirstOrDefault(x => x.IIASKey == courseId);
                if (branch != null && course != null && !branch.Courses.Contains(course))
                {
                    branch.Courses.Add(course);
                    SchedulerDatabase.Update(branch);
                }
            }

            _connection.Close();
        }
    }
}
