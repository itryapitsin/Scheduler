using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Models.Personalization
{
    public class UserRole: BaseEntity
    {
        public string Name { get; set; }

        public UserRoleType Type { get; set; }
    }
}
