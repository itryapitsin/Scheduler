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
    public class GroupMapping : EntityTypeConfiguration<Group>
    {
        public GroupMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId);

            HasRequired(x => x.Speciality)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.SpecialityId)
                .WillCascadeOnDelete(false);
        }
    }
}
