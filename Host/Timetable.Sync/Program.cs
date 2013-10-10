using System.Configuration;
using System.Data.Entity.Infrastructure;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Sync.Context;

namespace Timetable.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            //var conn = new OracleConnection("DATA SOURCE=iias.karelia.ru:1521/iias;USER ID=DPYATIN;Password=xgmst321");
            //try
            //{
            //    conn.Open();
            //    OracleCommand cmd = conn.CreateCommand();
            //    cmd.CommandText = "select cast(\"extend1\".\"ID\" as number(10,0)) as \"C1\", \"Extend1\".\"NAMEFULL\" as \"NAMEFULL\" from \"sdms\".\"B_BULDINGS\" \"Extend1\")";
            //    //cmd.CommandText = "select ID, NAMEFULL from sdms.B_BULDINGS";
            //    var reader = cmd.ExecuteReader();
            //    var data = reader.GetData(0);
            //}
            //catch (Exception)
            //{
            //}
            //finally
            //{
            //    conn.Close();
            //}

            using (var context = new IIASContext(new OracleConnection(ConfigurationManager.ConnectionStrings["OracleHR"].ConnectionString)))
            {
                var buildings = context.Buildings.AsEnumerable();
                //var result = context.RawSqlCommand("select * from V_ASP_ATTEST");
            }
        }
    }
}
