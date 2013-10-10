using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Timetable.Data.Context;

namespace Timetable.Sync.Context
{
    public class Building
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
    }

    public class IIASContext: BaseContext
    {
        public DbSet<Building> Buildings { get; set; }

        public IIASContext(OracleConnection connection):base(connection, true) {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var building = modelBuilder.Entity<Building>();
            building.ToTable("B_BULDINGS", "SDMS");
            building.Property(x => x.Id).HasColumnName("ID");//.HasColumnType("decimal(10)");
            building.Property(x => x.Fullname).HasColumnName("NAMEFULL");//.HasColumnType("nvarchar(250)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
