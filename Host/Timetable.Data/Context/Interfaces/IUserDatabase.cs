using System.Linq;
using Timetable.Base.Entities.Personalization;

namespace Timetable.Data.Context.Interfaces
{
    public interface IUserDatabase: IDatabase
    {
        IQueryable<User> Users { get; }

        IQueryable<UserRole> UserRoles { get; }
    }
}
