using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Mapping
{
    public class GroupMapping : EntityTypeConfiguration<Group>
    {
        public GroupMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(x => x.Faculties)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("GroupsToFaculties");
                    x.MapLeftKey("Group_Id");
                    x.MapRightKey("Faculty_Id");
                });

            HasMany(x => x.Courses)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("GroupsToCourses");
                    x.MapLeftKey("Group_Id");
                    x.MapRightKey("Course_Id");
                });

            HasMany(x => x.Specialities)
                .WithMany(x => x.Groups)
                .Map(x =>
                {
                    x.ToTable("GroupsToSpecialities");
                    x.MapLeftKey("Group_Id");
                    x.MapRightKey("Speciality_Id");
                });
        }
    }
}
