using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class GroupsToFacultiesSync: BaseSync
    {
        private DbConnection _connection;

        public GroupsToFacultiesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT 
                                    SDMS.V_STUD_GR.GR_BUN_ID AS groupid, 
                                    SDMS.V_STUD_GR.FACUL_BUN_ID AS FacultyId
                                FROM            
                                    SDMS.V_STUD_GR, SDMS.O_BASE_UNIT
                                WHERE        
                                    (SDMS.O_BASE_UNIT.STATUS = 'Y')";
            var reader = cmd.ExecuteReader();
            var groups = SchedulerDatabase.Groups.Include("Faculties").ToList();
            var faculties = SchedulerDatabase.Faculties.ToList();

            while (reader.Read())
            {
                var group = groups.FirstOrDefault(x => x.IIASKey == reader.GetInt64(0));
                var faculty = faculties.First(x => x.IIASKey == reader.GetInt64(1));
                if (group != null && faculty != null && !group.Faculties.Contains(faculty))
                {
                    group.Faculties.Add(faculty);
                    SchedulerDatabase.Update(group);
                }
            }

            _connection.Close();
        }
    }
}
