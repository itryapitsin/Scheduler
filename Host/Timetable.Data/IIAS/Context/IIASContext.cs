using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Context
{
    public class IIASContext: BaseContext, IIIASContext
    {
        public DbSet<Building> Buildings { get; set; }

        public IIASContext(OracleConnection connection) : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<Building> GetBuildings()
        {
            return RawSqlQuery<Building>(@"
                select 
                    ID as Id, 
                    NAMEFULL as Fullname,
                    NAMESHORT as Shortname,
                    ADDRESS as Address                    
                from SDMS.B_BULDINGS 
                where STATUS = 'Y'");
        }

        public IQueryable<AuditoriumType> GetAuditoriumType()
        {
            return RawSqlQuery<AuditoriumType>(@"
                
            ");
        }
    }
}
