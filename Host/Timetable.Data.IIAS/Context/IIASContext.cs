using System.Data.Entity;
using System.Linq;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Context
{
    public class IIASContext : BaseContext, IIIASContext
    {
        public DbSet<Building> Buildings { get; set; }

        public DbSet<Auditorium> Auditoriums { get; set; }

        public DbSet<Branche> Branches { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<Tutorial> Tutorials { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<TutorialType> TutorialTypes { get; set; }

        public DbSet<Time> Times { get; set; }

        public DbSet<ScheduleInfo> ScheduleInfoes { get; set; }

        public IIASContext(OracleConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<Building> GetBuildings()
        {
            return RawSqlQuery<Building>(@"
                    SELECT        
                        ID AS Id, 
                        NAMEFULL AS Fullname, 
                        NAMESHORT AS Shortname, 
                        ADDRESS AS Address
                    FROM            
                        SDMS.B_BULDINGS
                    WHERE        
                        (STATUS = 'Y')");
        }

        public IQueryable<AuditoriumType> GetAuditoriumTypes()
        {
            return RawSqlQuery<AuditoriumType>(@"");
        }

        public IQueryable<Branche> GetBranches()
        {
            return RawSqlQuery<Branche>(@"
                    SELECT DISTINCT 
                        SDMS.O_USE_BASE_UNITS.UBU_ID AS Id, 
                        SDMS.O_USE_BASE_UNITS.NAME_LONG AS Name,
                        SDMS.V_STUD_GR.ORG_ID AS OrganizationId
                    FROM
                        SDMS.O_USE_BASE_UNITS, SDMS.V_STUD_GR
                    WHERE
                        SDMS.O_USE_BASE_UNITS.UBU_ID = SDMS.V_STUD_GR.UBU_ID_PODR");
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return RawSqlQuery<Organization>(@"
                    SELECT        
                        ORG_ID as Id, 
                        NAME as Name
                    FROM
                        SDMS.O_ORGANIZATION");
        }

        public IQueryable<Faculty> GetFaculties()
        {
            return RawSqlQuery<Faculty>(@"
                    SELECT DISTINCT 
                        SDMS.V_STUD_GR.FACUL_BUN_ID AS Id, 
                        SDMS.V_STUD_GR.UBU_ID_PODR AS BranchId, 
                        SDMS.O_BASE_UNIT.NAME_SHORT AS ShortName, 
                        SDMS.O_BASE_UNIT.NAME_LONG AS Name
                    FROM
                        SDMS.O_BASE_UNIT, 
                        SDMS.V_STUD_GR
                    WHERE  
                        SDMS.O_BASE_UNIT.BUN_ID = SDMS.V_STUD_GR.FACUL_BUN_ID");
        }

        public IQueryable<Department> GetDepartments()
        {
            return RawSqlQuery<Department>(@"
                    SELECT DISTINCT 
                        SDMS.O_BASE_UNIT.BUN_ID AS Id, 
                        SDMS.O_BASE_UNIT.NAME_SHORT AS ShortName, 
                        SDMS.O_BASE_UNIT.NAME_LONG AS Name
                    FROM 
                        SDMS.V_PED_PERS_ALL,
                        SDMS.O_BASE_UNIT
                    WHERE  
                        SDMS.V_PED_PERS_ALL.BUN_ID = SDMS.O_BASE_UNIT.BUN_ID");
        }

        public IQueryable<Department> GetDepartments2(){
            return RawSqlQuery<Department>(@"
                    SELECT DISTINCT
                        SDMS.O_USE_BASE_UNITS.UBU_ID AS Id, 
                        SDMS.O_USE_BASE_UNITS.NAME_LONG AS Name, 
                        SDMS.O_USE_BASE_UNITS.NAME_SHORT AS ShortName
                    FROM            
                        SDMS.V_UPL_RASP, 
                        SDMS.O_USE_BASE_UNITS
                    WHERE        
                        SDMS.V_UPL_RASP.UBU_ID = SDMS.O_USE_BASE_UNITS.UBU_ID");
        }

        public IQueryable<Lecturer> GetLecturers()
        {
            return RawSqlQuery<Lecturer>(@"
                    SELECT DISTINCT 
                         SDMS.V_PED_PERS_ALL.PCARD_ID AS Id, 
                        SDMS.V_PED_PERS_ALL.I_NAME AS FirstName, 
                        SDMS.V_PED_PERS_ALL.O_NAME AS MiddleName, 
                        SDMS.V_PED_PERS_ALL.F_NAME AS LastName
                    FROM            
                        SDMS.V_PED_PERS_ALL, 
                        SDMS.O_BASE_UNIT
                    WHERE        
                        SDMS.V_PED_PERS_ALL.PCARD_ID = SDMS.O_BASE_UNIT.BUN_ID");
        }

        public IQueryable<Lecturer> GetLecturers2()
        {
            return RawSqlQuery<Lecturer>(@"
                    SELECT DISTINCT 
                         SDMS.O_PERSONAL_CARD.PCARD_ID AS Id, 
                        SDMS.O_PERSONAL_CARD.I_NAME AS FirstName, 
                        SDMS.O_PERSONAL_CARD.O_NAME AS MiddleName, 
                        SDMS.O_PERSONAL_CARD.F_NAME AS LastName
                    FROM            
                        SDMS.O_PERSONAL_CARD, 
                        SDMS.V_UPL_RASP
                    WHERE       
                        SDMS.O_PERSONAL_CARD.PCARD_ID = SDMS.V_UPL_RASP.PCARD_ID");
        }

        public IQueryable<Tutorial> GetTutorials()
        {
            return RawSqlQuery<Tutorial>(@"
                    SELECT DISTINCT 
                        DIS_CODE AS Id, 
                        DISCIPL_NAME AS Name
                    FROM            
                        SDMS.V_UPL_RASP");
        }

        public IQueryable<Speciality> GetSpecialities()
        {
            return RawSqlQuery<Speciality>(@"
                    SELECT DISTINCT 
                        SDMS.V_STUD_GR.SPEC_BUN_ID AS Id, 
                        SDMS.O_BASE_UNIT.NAME_SHORT AS ShortName, 
                        SDMS.O_BASE_UNIT.NAME_LONG AS Name, 
                        SDMS.O_BASE_UNIT.CODE AS Code
                    FROM            
                        SDMS.V_STUD_GR, 
                        SDMS.O_BASE_UNIT
                    WHERE        
                        SDMS.V_STUD_GR.SPEC_BUN_ID = SDMS.O_BASE_UNIT.BUN_ID");
        }

        public IQueryable<TutorialType> GetTutorialTypes()
        {
            return RawSqlQuery<TutorialType>(@"
                    SELECT DISTINCT 
                        EW_ID AS Id, 
                        KIND_EDU_WORK_NAME AS Name
                    FROM            
                        SDMS.V_UPL_RASP");
        }

        public IQueryable<Time> GetTimes()
        {
            return RawSqlQuery<Time>(@"
                    SELECT DISTINCT 
                        TUP_ID AS Id, 
                        TIME_FROM_PAR AS ""Start"",
                        TIME_TO_PAR AS ""Finish""
                    FROM            
                        SDMS.U_TYPE_UCHPAR
                    ORDER BY 
                        ""Start"", 
                        ""Finish""");
        }

        public IQueryable<Auditorium> GetAuditoriums()
        {
            return RawSqlQuery<Auditorium>(@"
                    SELECT        
                        SDMS.B_QUARTERS.ID AS Id, 
                        SDMS.B_QUARTERS.CODE AS Num, 
                        SDMS.B_QUARTERS.NAMEFULL AS Name, 
                        SDMS.B_QUARTERS.BLD_ID AS BuildingId
                    FROM    
                        SDMS.B_QUARTERS, 
                        SDMS.B_PRODUCTIVITY
                    WHERE
                        SDMS.B_QUARTERS.ID = SDMS.B_PRODUCTIVITY.ID 
                        AND (SDMS.B_QUARTERS.STATUS = 'Y')");
        }

        public IQueryable<ScheduleInfo> GetScheduleInfoes()
        {
            return RawSqlQuery<ScheduleInfo>(@"
                    SELECT DISTINCT 
                        CES_ID AS Id, 
                        MIN(HOURS_WEEK) AS HoursPerWeek, 
                        MIN(PCARD_ID) AS LecturerId, 
                        MIN(EW_ID) AS TutorialTypeId, 
                        MIN(DIS_CODE) AS TutorialId, 
                        MIN(UBU_ID) AS DepartmentId, 
                        UCH_GOG as StudyYear,
                        PERIOD_NUM As Semestr
                    FROM 
                        SDMS.V_UPL_RASP
                    WHERE        
                        (HOURS_WEEK IS NOT NULL) 
                        AND (PCARD_ID IS NOT NULL) 
                        AND (EW_ID IS NOT NULL) 
                        AND (DIS_CODE IS NOT NULL) 
                        AND (UBU_ID IS NOT NULL) 
                        AND (GR_UBU_ID > 0) 
                        AND (GR_UBU_ID IS NOT NULL) 
                        AND (KURS_CODE > 0) 
                        AND (KURS_CODE IS NOT NULL) 
                        AND (SPEC_CODE IS NOT NULL) 
                        AND (FACULT_CODE IS NOT NULL)
                    GROUP BY 
                        CES_ID, 
                        UCH_GOG,
                        PERIOD_NUM
                    ORDER BY 
                        UCH_GOG");
        }
    }
}
