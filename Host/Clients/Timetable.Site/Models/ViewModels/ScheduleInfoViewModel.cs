using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class ScheduleInfoViewModel
    {
        public int Id { get; set; }
        public string TutorialName { get; set; }
        public char TutorialType { get; set; }
        public string Lecturer { get; set; }
        public IEnumerable<string> Groups { get; set; }
        public decimal HoursPerWeek { get; set; }

        public decimal HoursPassed { get; set; }

        public int SubGroupCount { get; set; }
        public int TutorialId { get; set; }
        public int TutorialTypeId { get; set; }
        public int LecturerId { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }


        public ScheduleInfoViewModel(ScheduleInfoDataTransfer scheduleInfo)
        {
            Id = scheduleInfo.Id;
           

            if (scheduleInfo.Lecturer != null)
            {
                LecturerId = scheduleInfo.Lecturer.Id;
                Lecturer = LecturerViewModel.GetLecturerShortName(scheduleInfo.Lecturer);
            }

            HoursPerWeek = scheduleInfo.HoursPerWeek;
            HoursPassed = scheduleInfo.HoursPassed;
            Groups = scheduleInfo.Groups.Select(x => x.Code);
            GroupIds = scheduleInfo.Groups.Select(x => x.Id);
            SubGroupCount = scheduleInfo.SubgroupCount;

            if (scheduleInfo.StartDate.HasValue)
            {
                StartDate = scheduleInfo.StartDate.Value.ToString("yyyy-MM-dd");
            }
            if (scheduleInfo.EndDate.HasValue)
            {
                EndDate = scheduleInfo.EndDate.Value.ToString("yyyy-MM-dd");
            }

            if (scheduleInfo.Department != null)
            {
                DepartmentName = scheduleInfo.Department.Name;
                DepartmentId = scheduleInfo.Department.Id;
            }


            if (scheduleInfo.TutorialType != null)
            {
                TutorialType = scheduleInfo.TutorialType.Name.FirstOrDefault();
                TutorialTypeId = scheduleInfo.TutorialType.Id;
            }

            if (scheduleInfo.Tutorial != null)
            {
                TutorialName = scheduleInfo.Tutorial.ShortName;
                if (string.IsNullOrEmpty(TutorialName))
                    TutorialName = scheduleInfo.Tutorial.Name;
                TutorialId = scheduleInfo.Tutorial.Id;
            }
        }
    }
}