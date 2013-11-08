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
    public class TutorialMapping : EntityTypeConfiguration<Tutorial>
    {
        public TutorialMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(x => x.Faculties)
                .WithMany(x => x.Tutorials)
                .Map(x =>
                {
                    x.MapLeftKey("Tutorial_Id");
                    x.MapRightKey("Faculty_Id");
                    x.ToTable("FacultiesToTutorials");
                });


            HasMany(x => x.Specialities)
                .WithMany(x => x.Tutorials)
                .Map(x =>
                {
                    x.MapLeftKey("Tutorial_Id");
                    x.MapRightKey("Speciality_Id");
                    x.ToTable("SpecialitiesToTutorials");
                });

            HasMany(x => x.ScheduleInfoes)
                .WithRequired(x => x.Tutorial)
                .HasForeignKey(x => x.TutorialId);
        }
    }
}
