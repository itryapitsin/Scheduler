using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Timetable.Data.Context;
using Timetable.Data.Models.Personalization;
using Timetable.Logic.Models;

namespace Timetable.Logic.Services
{
    public class UserService
    {
        protected SchedulerContext DataContext = new SchedulerContext();

        public void CreateUser(
            string login, 
            string password, 
            string firstname,
            string middlename,
            string lastname,
            UserRoleType roleType)
        {
            var role = DataContext.UserRoles.FirstOrDefault(x => x.Type == roleType);
            var hasEqualsLogin = DataContext.Users.Any(x => x.Login == login);
            if(hasEqualsLogin)
                throw new DuplicateNameException(login);

            DataContext.Users.Add(new User
                {
                    Login = login,
                    Password = password,
                    Firstname = firstname,
                    Middlename = middlename,
                    Lastname = lastname,
                    Role = role,
                    IsActual = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });

            DataContext.SaveChanges();
        }

        public IEnumerable<UserDataTransfer> GetUsers(IEnumerable<string> exclude)
        {
            return DataContext.Users
                .Where(x => exclude.Any(y => y != x.Login))
                .Include(x => x.Role)
                .ToList()
                .Select(x => new UserDataTransfer(x));
        }

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

        public void SaveUserState(UserDataTransfer userDataTransfer, string password = null)
        {
            var user = DataContext.Users.Include(x => x.CreatorSelectedGroups).First(x => x.Login == userDataTransfer.Login);

            if (!string.IsNullOrEmpty(password))
                user.Password = password;

            user.Firstname = userDataTransfer.Firstname;
            user.Middlename = userDataTransfer.Middlename;
            user.Lastname = userDataTransfer.Lastname;
            user.CreatorSelectedBuildingId = userDataTransfer.CreatorSettings.CurrentBuildingId;
            user.CreatorSelectedSemesterId = userDataTransfer.CreatorSettings.CurrentSemesterId;
            user.CreatorSelectedStudyYearId = userDataTransfer.CreatorSettings.CurrentStudyYearId;
            user.CreatorSelectedBranchId = userDataTransfer.CreatorSettings.CurrentBranchId;
            user.CreatorSelectedFacultyId = userDataTransfer.CreatorSettings.CurrentFacultyId;
            user.CreatorSelectedStudyTypeId = userDataTransfer.CreatorSettings.CurrentStudyTypeId;
            if (user.CreatorSelectedFacultyId.HasValue)
                user.CreatorSelectedBranchId = DataContext.Branches.First(x => x.Faculties.Any(y => y.Id == user.CreatorSelectedFacultyId)).Id;

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
