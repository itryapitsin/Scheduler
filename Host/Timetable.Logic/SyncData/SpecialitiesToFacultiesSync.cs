﻿using System;
using System.Data.Common;
using System.Linq;

namespace Timetable.Sync.Logic.SyncData
{
    public class SpecialitiesToFacultiesSync: BaseSync
    {
        private DbConnection _connection;

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

            while (reader.Read())
            {
                var specialityId = reader.GetInt64(0);
                var schedulerEntity = schedulerEntities.FirstOrDefault(x => x.IIASKey == specialityId);
                var id = reader.GetInt64(1);
                var faculty = SchedulerDatabase.Faculties.First(x => x.IIASKey == id);
                if (schedulerEntity != null && faculty != null && !schedulerEntity.Faculties.Contains(faculty))
                {
                    schedulerEntity.Faculties.Add(faculty);
                    SchedulerDatabase.Update(schedulerEntity);
                }
            }

            _connection.Close();
        }
    }
}
