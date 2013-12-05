using System;
using System.Linq;
using Timetable.Data.Models.Personalization;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Logic.Models
{
    public class UserDataTransfer
    {
        public UserRoleType Type { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public CreatorSettings CreatorSettings { get; set; }
        public LecturerScheduleSettings LecturerScheduleSettings { get; set; }
        public AuditoriumScheduleSettings AuditoriumScheduleSettings { get; set; }
        public UserDataTransfer(User user)
        {
            Login = user.Login;
            Type = user.Role.Type;
            Firstname = user.Firstname;
            Middlename = user.Middlename;
            Lastname = user.Lastname;

            CreatorSettings = new CreatorSettings(user);
            LecturerScheduleSettings = new LecturerScheduleSettings(user);
            AuditoriumScheduleSettings = new AuditoriumScheduleSettings(user);
        }

        public string GetUserName()
        {
            if (!String.IsNullOrEmpty(Firstname) && !String.IsNullOrEmpty(Middlename) && !String.IsNullOrEmpty(Lastname))
                return string.Format("{0} {1}. {2}.", Lastname, Firstname[0], Middlename[0]);

            if (!String.IsNullOrEmpty(Firstname) && !String.IsNullOrEmpty(Lastname))
                return string.Format("{0} {1}.", Lastname, Firstname[0]);

            return Login;
        }
    }
}
