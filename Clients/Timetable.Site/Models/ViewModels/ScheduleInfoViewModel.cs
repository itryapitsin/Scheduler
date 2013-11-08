using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.DataService;

namespace Timetable.Site.Models.ViewModels
{
    public class ScheduleInfoViewModel
    {
        public int Id { get; set; }
        public string TutorialName { get; set; }

        public string TutorialType { get; set; }

        public string Lecturer { get; set; }

        public IEnumerable<string> Groups { get; set; }

        public int HoursLeft { get; set; }

        public int HoursPassed { get; set; }

        public ScheduleInfoViewModel(ScheduleInfo scheduleInfo)
        {
            Id = scheduleInfo.Id;
            TutorialName = scheduleInfo.Tutorial.ShortName;
            TutorialType = scheduleInfo.TutorialType.Name;
            Lecturer = string.Format("{0} {1}.{2}.", 
                scheduleInfo.Lecturer.Lastname, 
                scheduleInfo.Lecturer.Firstname.FirstOrDefault(),
                scheduleInfo.Lecturer.Middlename.FirstOrDefault());

            HoursLeft = scheduleInfo.HoursPerWeek;
            //HoursPassed = scheduleInfo.Schedules.Sum(x => x.)
            Groups = scheduleInfo.Groups.Select(x => x.Code);
        }
    }
}