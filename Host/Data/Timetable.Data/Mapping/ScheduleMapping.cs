using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Mapping
{
    public class ScheduleMapping : EntityTypeConfiguration<Schedule>
    {
        public ScheduleMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(x => x.Auditorium)
                .WithMany()
                .HasForeignKey(x => x.AuditoriumId);

            HasRequired(x => x.Time)
                .WithMany()
                .HasForeignKey(x => x.TimeId);

            HasRequired(x => x.ScheduleInfo)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.ScheduleInfoId);

            HasRequired(x => x.WeekType)
                .WithMany()
                .HasForeignKey(x => x.WeekTypeId);
        }
    }
}
