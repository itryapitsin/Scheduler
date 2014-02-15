using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Models.Scheduler;
using Timetable.Site.Infrastructure;

namespace Timetable.Site.Models.ViewModels
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
        public TimeViewModel Time { get; set; }
        public int TimeId { get; set; }
        public int Pair { get; set; }
        public int DayOfWeek { get; set; }

        public int WeekTypeId { get; set; }
        public int BuildingId { get; set; }
        public int AuditoriumId { get; set; }
        public int ScheduleInfoId { get; set; }
        public bool AutoDelete { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string ScheduleTypeName { get; set; }
        public int ScheduleTypeId { get; set; }

        public ScheduleViewModel(ScheduleDataTransfer schedule)
        {
            var cns = new CutNameService();
        
            Id = schedule.Id;
            AuditoriumNumber = schedule.Auditorium.Number;
            BuildingName = schedule.Auditorium.Building.ShortName;
            LecturerName = LecturerViewModel.GetLecturerShortName(schedule.ScheduleInfo.Lecturer);
            TutorialName = cns.Cut(schedule.ScheduleInfo.Tutorial.Name);
            TutorialTypeName = schedule.ScheduleInfo.TutorialType.Name.FirstOrDefault();
            GroupCodes = schedule.ScheduleInfo.Groups.Select(x => x.Code);
            WeekTypeName = schedule.WeekType.Name;
            SubGroup = schedule.SubGroup;
            TimeId = schedule.Time.Id;
            Time = new TimeViewModel(schedule.Time);
            Pair = schedule.Time.Position;
            DayOfWeek = schedule.DayOfWeek;

            WeekTypeId = schedule.WeekType.Id;
            BuildingId = schedule.Auditorium.Building.Id;
            AuditoriumId = schedule.Auditorium.Id;
            ScheduleInfoId = schedule.ScheduleInfo.Id;
            AutoDelete = schedule.AutoDelete;
            StartDate = schedule.StartDate.ToString("yyyy-MM-dd");
            EndDate = schedule.EndDate.ToString("yyyy-MM-dd");

            ScheduleTypeName = schedule.ScheduleType.Name;
            ScheduleTypeId = schedule.ScheduleType.Id;

        }
    }
}