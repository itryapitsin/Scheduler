using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Site.Controllers.Extends;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string AuditoriumNumber { get; set; }
        public string BuildingName { get; set; }
        public string LecturerName { get; set; }
        public string TutorialName { get; set; }
        public string TutorialTypeName { get; set; }
        public IEnumerable<string> GroupCodes { get; set; }
        public string WeekTypeName { get; set; }
        public string SubGroup { get; set; }

        public ScheduleViewModel(ScheduleDataTransfer schedule)
        {
            var cns = new CutNameService();

            Id = schedule.Id;
            AuditoriumNumber = schedule.Auditorium.Number;
            BuildingName = schedule.Auditorium.Building.Name;
            LecturerName = String.Format("{0} {1}. {2}.",
                schedule.ScheduleInfo.Lecturer.Lastname,
                schedule.ScheduleInfo.Lecturer.Firstname[0],
                schedule.ScheduleInfo.Lecturer.Middlename[0]);
            TutorialName = cns.Cut(schedule.ScheduleInfo.Tutorial.Name);
            TutorialTypeName = schedule.ScheduleInfo.TutorialType.Name;
            GroupCodes = schedule.ScheduleInfo.Groups.Select(x => x.Code);
            WeekTypeName = schedule.WeekType.Name;
            SubGroup = schedule.SubGroup;
        }
    }
}