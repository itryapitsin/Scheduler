using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class GroupsToCoursesSync: BaseSync
    {
        private DbConnection _connection;

        public GroupsToCoursesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT 
                                    SDMS.V_STUD_GR.GR_BUN_ID AS groupid, 
                                    SDMS.V_STUD_GR.KURS_BUN_ID as courseid
                                FROM           
                                    SDMS.V_STUD_GR, SDMS.O_BASE_UNIT
                                WHERE
                                    (SDMS.O_BASE_UNIT.STATUS = 'Y')";
            var reader = cmd.ExecuteReader();
            var groups = SchedulerDatabase.Groups.Include("Courses").ToList();
            var courses = SchedulerDatabase.Courses.ToList();

            while (reader.Read())
            {
                var group = groups.FirstOrDefault(x => x.IIASKey == reader.GetInt64(0));
                var course = courses.First(x => x.IIASKey == reader.GetInt64(1));
                if (group != null && course != null && !group.Courses.Contains(course))
                {
                    group.Courses.Add(course);
                    SchedulerDatabase.Update(group);
                }
            }

            _connection.Close();
        }
    }
}
