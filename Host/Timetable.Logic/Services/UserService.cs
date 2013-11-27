using System.Data.Entity;
using System.Linq;
using Timetable.Data.Context;
using Timetable.Logic.Models;

namespace Timetable.Logic.Services
{
    public class UserService
    {
        protected SchedulerContext DataContext = new SchedulerContext();

        public bool Validate(string login, string password)
        {
            return DataContext.Users.FirstOrDefault(x => x.Login == login && x.Password == password) != null;
        }

        public UserDataTransfer FindUser(string login)
        {
            return new UserDataTransfer(DataContext.Users
                .Include(x => x.CreatorSelectedGroups)
                .Include(x => x.AuditoriumScheduleSelectedAuditoriumTypes)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Login == login));
        }

        public void SaveUserState(UserDataTransfer userDataTransfer)
        {
            var user = DataContext.Users.Include(x => x.CreatorSelectedGroups).First(x => x.Login == userDataTransfer.Login);
            
            user.CreatorSelectedBuildingId = userDataTransfer.CreatorSettings.CurrentBuildingId;
            user.CreatorSelectedSemesterId = userDataTransfer.CreatorSettings.CurrentSemesterId;
            user.CreatorSelectedStudyYearId = userDataTransfer.CreatorSettings.CurrentStudyYearId;
            user.CreatorSelectedBranchId = userDataTransfer.CreatorSettings.CurrentBranchId;
            user.CreatorSelectedFacultyId = userDataTransfer.CreatorSettings.CurrentFacultyId;
            user.CreatorSelectedCourseId = userDataTransfer.CreatorSettings.CurrentCourseId;

            foreach (var @group in user.CreatorSelectedGroups.ToArray())
                user.CreatorSelectedGroups.Remove(@group);

            var groups = DataContext.Groups
                .Where(x => userDataTransfer.CreatorSettings.CurrentGroupIds.Contains(x.Id))
                .ToList();
            
            user.CreatorSelectedGroups.AddRange(groups);

            user.AuditoriumScheduleSelectedBuildingId = userDataTransfer.AuditoriumScheduleSettings.BuildingId;
            user.AuditoriumScheduleSelectedAuditoriumId = userDataTransfer.AuditoriumScheduleSettings.AuditoriumId;
            user.AuditoriumScheduleSelectedStudyYearId = userDataTransfer.AuditoriumScheduleSettings.StudyYearId;
            user.AuditoriumScheduleSelectedSemesterId = userDataTransfer.AuditoriumScheduleSettings.SemesterId;

            user.LecturerScheduleSelectedLecturerId = userDataTransfer.LecturerScheduleSettings.LecturerId;
            user.LecturerScheduleSelectedSemesterId = userDataTransfer.LecturerScheduleSettings.SemesterId;
            user.LecturerScheduleSelectedStudyYearId = userDataTransfer.LecturerScheduleSettings.StudyYearId;

            var auditoriumTypes = DataContext.AuditoriumTypes
                .Where(x => userDataTransfer.AuditoriumScheduleSettings.AuditoriumTypeIds.Contains(x.Id))
                .ToList();

            foreach (var auditoriumType in user.AuditoriumScheduleSelectedAuditoriumTypes.ToArray())
                user.AuditoriumScheduleSelectedAuditoriumTypes.Remove(auditoriumType);

            user.AuditoriumScheduleSelectedAuditoriumTypes.AddRange(auditoriumTypes);

            DataContext.Update(user);
        }
    }
}
