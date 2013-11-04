using System.Data.Entity;
using System.Linq;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Context
{
    public class IIASContext: BaseContext, IIIASContext
    {
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Branche> Branches { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }


        public IIASContext(OracleConnection connection) : base(connection, true)
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

        public IQueryable<AuditoriumType> GetAuditoriumType()
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
                        SDMS.V_STUD_GR, 
                        SDMS.O_USE_BASE_UNITS
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
    }
}
