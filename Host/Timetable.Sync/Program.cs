using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection conn = new OracleConnection("DATA SOURCE=iias.karelia.ru:1521/iias;USER ID=DPYATIN;Password=xgmst321");
            try
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from V_ASP_ATTEST";
                var reader = cmd.ExecuteReader();
                var data = reader.GetData(0);
            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
