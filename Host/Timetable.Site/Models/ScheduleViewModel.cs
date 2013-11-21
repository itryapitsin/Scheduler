using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Models.Scheduler;
using Timetable.Site.Controllers.Extends;


namespace Timetable.Site.Models
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string AuditoriumNumber { get; set; }
        public string BuildingName { get; set; }
        public string LecturerName { get; set; }
        public string TutorialName { get; set; }
        public char TutorialTypeName { get; set; }
        public IEnumerable<string> GroupCodes { get; set; }
        public string WeekTypeName { get; set; }
        public string SubGroup { get; set; }
        public int TimeId { get; set; }
        public int Pair { get; set; }
        public int DayOfWeek { get; set; }

        public ScheduleViewModel(ScheduleDataTransfer schedule)
        {
            var cns = new CutNameService();

            Id = schedule.Id;
            AuditoriumNumber = schedule.Auditorium.Number;
            BuildingName = schedule.Auditorium.Building.ShortName;
            LecturerName = schedule.ScheduleInfo.Lecturer.Lastname;

            if (schedule.ScheduleInfo.Lecturer.Firstname != null)
                LecturerName += " " + schedule.ScheduleInfo.Lecturer.Firstname.FirstOrDefault() + ".";

            if (schedule.ScheduleInfo.Lecturer.Middlename != null)
                LecturerName += schedule.ScheduleInfo.Lecturer.Middlename.FirstOrDefault() + ".";

            TutorialName = cns.Cut(schedule.ScheduleInfo.Tutorial.Name);
            TutorialTypeName = schedule.ScheduleInfo.TutorialType.Name.FirstOrDefault();
            GroupCodes = schedule.ScheduleInfo.Groups.Select(x => x.Code);
            WeekTypeName = schedule.WeekType.Name;
            SubGroup = schedule.SubGroup;
            TimeId = schedule.Time.Id;
            Pair = schedule.Time.Position;
            DayOfWeek = schedule.DayOfWeek;
        }
    }
}