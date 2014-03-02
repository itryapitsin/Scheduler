using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Infrastructure
{
    public class SchedulePresentationFormatService
    {
        public List<ScheduleViewModel> ForAuditoriumFilter(IEnumerable<ScheduleViewModel> schedules)
        {
            //var t = schedules.GroupBy(x => new { x.LecturerName, x.DayOfWeek, x.TimeId, x.WeekTypeId }).ToList();

            var ans = new List<ScheduleViewModel>();

            var hash = new HashSet<int>();

            foreach (var s in schedules)
            {
                if (!hash.Any(x => x == s.Id))
                    ans.Add(s);


                var t = schedules.Where(x => x.LecturerName == s.LecturerName &&
                                         x.TutorialName == s.TutorialName &&
                                         x.DayOfWeek == s.DayOfWeek && x.TimeId == s.TimeId &&
                                         x.Id != s.Id).ToList();
                if (s.WeekTypeId != 2)
                    t = t.Where(x => x.WeekTypeId == s.WeekTypeId).ToList();


                foreach (var tt in t)
                {
                    s.GroupCodes = s.GroupCodes.Union(tt.GroupCodes);
                    if (tt.SubGroup != "")
                        s.SubGroup += " " + tt.SubGroup;
                    hash.Add(tt.Id);
                }
            }

            return ans;
        }


        public List<ScheduleViewModel> ForGroupFilter(IEnumerable<ScheduleViewModel> schedules)
        {
            //var t = schedules.GroupBy(x => new { x.LecturerName, x.DayOfWeek, x.TimeId, x.WeekTypeId }).ToList();

            var ans = new List<ScheduleViewModel>();

            var hash = new HashSet<int>();

            foreach (var s in schedules)
            {
                if (!hash.Any(x => x == s.Id))
                    ans.Add(s);


                var t = schedules.Where(x => x.LecturerName == s.LecturerName &&
                                         x.TutorialName == s.TutorialName &&
                                         x.DayOfWeek == s.DayOfWeek && x.TimeId == s.TimeId &&
                                         x.Id != s.Id).ToList();
                if (s.WeekTypeId != 2)
                    t = t.Where(x => x.WeekTypeId == s.WeekTypeId).ToList();

                foreach (var tt in t)
                {
                    s.GroupCodes = s.GroupCodes.Union(tt.GroupCodes);
                    if (tt.SubGroup != "")
                        s.SubGroup += " " + tt.SubGroup;
                    hash.Add(tt.Id);
                }
            }

            return ans;
        }
    }
}