using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class SpecialitiesToFacultiesSync: BaseSync
    {
        private DbConnection _connection;
        private string _commandPattern = @"
INSERT INTO [dbo].[SpecialitiesToFaculties]
           ([Faculty_Id]
           ,[Speciality_Id])
VALUES
           ({0}
           ,{1});";

        public SpecialitiesToFacultiesSync(DbConnection conn)
        {
            _connection = conn;
        }

        public override void Sync()
        {
            _connection.Open();
            DbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT DISTINCT 
                    SDMS.V_STUD_GR.SPEC_BUN_ID AS Speciality_Id, 
                    SDMS.V_STUD_GR.FACUL_BUN_ID AS Faculty_Id
                FROM            
                    SDMS.V_STUD_GR, 
                    SDMS.O_BASE_UNIT
                WHERE       
                    SDMS.V_STUD_GR.SPEC_BUN_ID = SDMS.O_BASE_UNIT.BUN_ID";
            var reader = cmd.ExecuteReader();
            var schedulerEntities = SchedulerDatabase.Specialities.Include("Faculties").ToList();
            var faculties = SchedulerDatabase.Faculties.ToList();
            var command = String.Empty;

            while (reader.Read())
            {
                var specialityId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == specialityId);
                var id = reader.GetInt64(1);
                var faculty = faculties.FirstOrDefault(x => x.IIASKey == id);
                if (schedulerEntity != null && faculty != null && !schedulerEntity.Faculties.Contains(faculty))
                {
                    schedulerEntity.Faculties.Add(faculty);
                    //SchedulerDatabase.Update(schedulerEntity);
                    command += String.Format(_commandPattern, faculty.Id, schedulerEntity.Id);
                }
            }

            if(!String.IsNullOrEmpty(command))
                SchedulerDatabase.RawSqlCommand(command);

            _connection.Close();
        }
    }
}
