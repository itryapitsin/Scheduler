using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Timetable.Base.Entities;
using Timetable.Data.Context.Interfaces;
using Timetable.Data.Models;

namespace Timetable.Data.Context
{
    public abstract class BaseContext : DbContext, IDataContext
    {
        /// <summary>
        /// Return models error
        /// </summary>
        /// <returns></returns>
        
    }
}