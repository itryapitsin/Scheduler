using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Timetable.Data.IIAS.Models;

namespace Timetable.Data.IIAS.Mapping
{
    public class BuildingMapping : EntityTypeConfiguration<Building>
    {
        public BuildingMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("B_BULDINGS", "SDMS");
            Property(x => x.Id).HasColumnName("ID");
            Property(x => x.Fullname).HasColumnName("NAMEFULL");
        }
    }
}
