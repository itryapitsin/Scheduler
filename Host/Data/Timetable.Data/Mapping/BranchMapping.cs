using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Mapping
{
    public class BranchMapping : EntityTypeConfiguration<Branch>
    {
        public BranchMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(x => x.Courses)
                .WithMany(x => x.Branches)
                .Map(x =>
                {
                    x.ToTable("BranchesToCourses");
                    x.MapLeftKey("Branch_Id");
                    x.MapRightKey("Course_Id");
                });
        }
    }
}
