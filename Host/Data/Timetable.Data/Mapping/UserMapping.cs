using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Timetable.Data.Models.Personalization;

namespace Timetable.Data.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(x => x.CreatorSelectedGroups)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("GroupsToUsers");
                    x.MapLeftKey("User_Id");
                    x.MapRightKey("Group_Id");
                });

            HasMany(x => x.AuditoriumScheduleSelectedAuditoriumTypes)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("AuditoriumTypesToUsers");
                    x.MapLeftKey("User_Id");
                    x.MapRightKey("AuditoriumType_Id");
                });
        }
    }
}
