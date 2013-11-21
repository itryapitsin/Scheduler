
using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Models.Scheduler;
using Timetable.Site.Controllers.Extends;

namespace Timetable.Site.Models.Schedules
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public int ScheduleInfoId { get; set; }
        public int DayOfWeek { get; set; }
        public int PeriodId { get; set; }
        public int BuildingId { get; set; }
        public string WeekTypeName { get; set; }
        public int WeekTypeId { get; set; }
        public string AuditoriumNumber { get; set; }
        public int AuditoriumId { get; set; }
        public string LecturerName { get; set; }
        public int LecturerId { get; set; }
        public string TutorialName { get; set; }
        public int TutorialId { get; set; }
        public string TutorialTypeName { get; set; }
        public int TutorialTypeId { get; set; }
        public string GroupNames { get; set; }
        public string GroupIds { get; set; }
        public bool IsForLecturer { get; set; }
        public bool IsForAuditorium { get; set; }
        public bool IsForGroup { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool AutoDelete { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LecturerFullName { get; set; }
        public string TutorialFullName { get; set; }
        public string TutorialTypeFullName { get; set; }
        public string WeekTypeFullName { get; set; }
        public string BuildingFullName { get; set; }
        public string SubGroup { get; set; }

        public ScheduleViewModel(ScheduleDataTransfer schedule)
        {
            var cns = new CutNameService();

            Id = schedule.Id;
            DayOfWeek = schedule.DayOfWeek;
            StartDate = schedule.StartDate.ToShortDateString();
            EndDate = schedule.EndDate.ToShortDateString();
            AutoDelete = schedule.AutoDelete;
            SubGroup = schedule.SubGroup;
            PeriodId = schedule.Time.Id;

            if (schedule.WeekType != null)
            {
                WeekTypeId = schedule.WeekType.Id;
                WeekTypeName = schedule.WeekType.Name;
                if (WeekTypeName == "Л") { WeekTypeFullName = "Еженедельно"; }
                if (WeekTypeName == "Ч") { WeekTypeFullName = "Числитель"; }
                if (WeekTypeName == "З") { WeekTypeFullName = "Знаменатель"; }
            }

            if (schedule.Auditorium != null)
            {
                AuditoriumId = schedule.Auditorium.Id;
                AuditoriumNumber = schedule.Auditorium.Number;

                if (schedule.Auditorium.Building != null)
                {
                    BuildingFullName = schedule.Auditorium.Building.Name;
                    BuildingId = schedule.Auditorium.Building.Id;
                }
            }

            if (schedule.ScheduleInfo != null)
            {
                ScheduleInfoId = schedule.ScheduleInfo.Id;

                if (schedule.ScheduleInfo.Lecturer != null)
                {
                    LecturerId = schedule.ScheduleInfo.Lecturer.Id;
                    LecturerName = schedule.ScheduleInfo.Lecturer.Lastname;// +" " + schedule.ScheduleInfo.Lecturer.Firstname[0] + ". " + schedule.ScheduleInfo.Lecturer.Middlename[0] + ".";
                    LecturerFullName = schedule.ScheduleInfo.Lecturer.Lastname + " " + schedule.ScheduleInfo.Lecturer.Firstname + " " + schedule.ScheduleInfo.Lecturer.Middlename + " ";
                }

                if (schedule.ScheduleInfo.Tutorial != null)
                {
                    TutorialId = schedule.ScheduleInfo.Tutorial.Id;
                    TutorialName = cns.Cut(schedule.ScheduleInfo.Tutorial.Name);
                    TutorialFullName = schedule.ScheduleInfo.Tutorial.Name;
                }

                if (schedule.ScheduleInfo.TutorialType != null)
                {
                    TutorialTypeId = schedule.ScheduleInfo.TutorialType.Id;
                    TutorialTypeName = schedule.ScheduleInfo.TutorialType.Name;
                    TutorialTypeFullName = schedule.ScheduleInfo.TutorialType.Name;
                    if (TutorialTypeId == 1) TutorialTypeName = "Лек.";
                    if (TutorialTypeId == 2) TutorialTypeName = "Лаб.";
                    if (TutorialTypeId == 3) TutorialTypeName = "Пр.";
                    if (TutorialTypeId == 20) TutorialTypeName = "Сем-р.";
                    if (TutorialTypeId == 22) TutorialTypeName = "Фак-в.";
                    if (TutorialTypeId == 23) TutorialTypeName = "Инд.";
                    if (TutorialTypeId == 43) TutorialTypeName = "Пр.в диспл.";
                    if (TutorialTypeId == 44) TutorialTypeName = "Лаб.(изл.)";

                }

                if (schedule.ScheduleInfo.Groups != null)
                {
                    foreach (var g in schedule.ScheduleInfo.Groups)
                    {
                        if (g.Code != null)
                            GroupNames += g.Code + " ";
                        GroupIds += g.Id + ", ";
                    }
                }
            }



        }

        public ScheduleViewModel(Schedule schedule, bool isForLecturer, bool isForAuditorium, bool isForGroup)
        {
            var cns = new CutNameService();

            IsForLecturer = isForLecturer;
            IsForAuditorium = isForAuditorium;
            IsForGroup = isForGroup;
            Id = schedule.Id;
            DayOfWeek = schedule.DayOfWeek;
            StartDate = schedule.StartDate.ToShortDateString();
            EndDate = schedule.EndDate.ToShortDateString();
            AutoDelete = schedule.AutoDelete;
            StartTime = schedule.Time.Start.ToString(@"hh\:mm");
            EndTime = schedule.Time.End.ToString(@"hh\:mm");
            SubGroup = schedule.SubGroup;
            
            if (schedule.Time != null)
                PeriodId = schedule.Time.Id;

            if (schedule.WeekType != null)
            {
                WeekTypeId = schedule.WeekType.Id;
                WeekTypeName = schedule.WeekType.Name;
                if (WeekTypeName == "Л") { WeekTypeFullName = "Еженедельно"; }
                if (WeekTypeName == "Ч") { WeekTypeFullName = "Числитель"; }
                if (WeekTypeName == "З") { WeekTypeFullName = "Знаменатель"; }
            }

            if (schedule.Auditorium != null)
            {
                AuditoriumId = schedule.Auditorium.Id;
                AuditoriumNumber = schedule.Auditorium.Number;

                if (schedule.Auditorium.Building != null)
                {
                    BuildingFullName = schedule.Auditorium.Building.Name;
                    BuildingId = schedule.Auditorium.Building.Id;
                }
            }

            if (schedule.ScheduleInfo != null)
            {
                ScheduleInfoId = schedule.ScheduleInfo.Id;

                if (schedule.ScheduleInfo.Lecturer != null)
                {
                    LecturerId = schedule.ScheduleInfo.Lecturer.Id;
                    LecturerName = schedule.ScheduleInfo.Lecturer.Lastname;// +" " + schedule.ScheduleInfo.Lecturer.Firstname[0] + ". " + schedule.ScheduleInfo.Lecturer.Middlename[0] + ".";
                    LecturerFullName = schedule.ScheduleInfo.Lecturer.Lastname +" " + schedule.ScheduleInfo.Lecturer.Firstname + " " + schedule.ScheduleInfo.Lecturer.Middlename + " ";
                }
            
                if (schedule.ScheduleInfo.Tutorial != null)
                {
                    TutorialId = schedule.ScheduleInfo.Tutorial.Id;
                    TutorialName = cns.Cut(schedule.ScheduleInfo.Tutorial.Name);
                    TutorialFullName = schedule.ScheduleInfo.Tutorial.Name;
                }
            
                if (schedule.ScheduleInfo.TutorialType != null)
                {
                    TutorialTypeId = schedule.ScheduleInfo.TutorialType.Id;
                    TutorialTypeName = schedule.ScheduleInfo.TutorialType.Name;
                    TutorialTypeFullName = schedule.ScheduleInfo.TutorialType.Name;
                    if (TutorialTypeId == 1) TutorialTypeName = "Лек.";
                    if (TutorialTypeId == 2) TutorialTypeName = "Лаб.";
                    if (TutorialTypeId == 3) TutorialTypeName = "Пр.";
                    if (TutorialTypeId == 20) TutorialTypeName = "Сем-р.";
                    if (TutorialTypeId == 22) TutorialTypeName = "Фак-в.";
                    if (TutorialTypeId == 23) TutorialTypeName = "Инд.";
                    if (TutorialTypeId == 43) TutorialTypeName = "Пр.в диспл.";
                    if (TutorialTypeId == 44) TutorialTypeName = "Лаб.(изл.)";

                }
            
                if (schedule.ScheduleInfo.Groups != null)
                {
                    foreach (var g in schedule.ScheduleInfo.Groups)
                    {
                        if (g.Code != null)
                            GroupNames += g.Code + " ";
                        GroupIds += g.Id + ", ";
                    }
                }
            }

            

        }
    }
}