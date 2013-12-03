using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Mapping
{
    public class DepartmentMapping : EntityTypeConfiguration<Department>
    {
        public DepartmentMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(x => x.Faculties)
                .WithMany(x => x.Departments)
                .Map(x =>
                {
                    x.ToTable("DepartmentsToFaculties");
                    x.MapLeftKey("Department_Id");
                    x.MapRightKey("Faculty_Id");
                });
        }
    }
}
