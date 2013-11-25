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

        public decimal HoursTotal { get; set; }

        public ScheduleInfoViewModel(ScheduleInfoDataTransfer scheduleInfo)
        {
            Id = scheduleInfo.Id;
            TutorialName = scheduleInfo.Tutorial.ShortName;
            if (string.IsNullOrEmpty(TutorialName))
                TutorialName = scheduleInfo.Tutorial.Name;

            TutorialType = scheduleInfo.TutorialType.Name.FirstOrDefault();
            Lecturer = LecturerViewModel.GetLecturerShortName(scheduleInfo.Lecturer);
            HoursPerWeek = scheduleInfo.HoursPerWeek;
            //HoursPassed = scheduleInfo.Schedules.Sum(x => x.)
            Groups = scheduleInfo.Groups.Select(x => x.Code);
        }
    }
}