using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;

namespace Timetable.Sync.Context
{
    public class IIASContext: BaseContext
    {
        public IIASContext(OracleConnection connection):base(connection, true) {}
    }
}
