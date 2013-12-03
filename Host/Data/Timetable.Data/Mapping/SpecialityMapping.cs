using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Mapping
{
    public class SpecialityMapping : EntityTypeConfiguration<Speciality>
    {
        public SpecialityMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
