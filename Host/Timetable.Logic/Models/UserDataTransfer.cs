﻿using System.Linq;
using Timetable.Data.Models.Personalization;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Logic.Models
{
    public class UserDataTransfer
    {
        public string Login { get; set; }
        public CreatorSettings CreatorSettings { get; set; }
        public LecturerScheduleSettings LecturerScheduleSettings { get; set; }
        public AuditoriumScheduleSettings AuditoriumScheduleSettings { get; set; }
        public UserDataTransfer(User user)
        {
            Login = user.Login;

            CreatorSettings = new CreatorSettings(user);
            LecturerScheduleSettings = new LecturerScheduleSettings(user);
            AuditoriumScheduleSettings = new AuditoriumScheduleSettings(user);
        }
    }
}