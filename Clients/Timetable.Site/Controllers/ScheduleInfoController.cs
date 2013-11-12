using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.Site.DataService;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{
    public class ScheduleInfoController : BaseController
    {
        public ActionResult Get(int facultyId, string courseIdsStr, string groupIdsStr, int studyYearId, int semestr)
        {
            var result = new List<ScheduleInfoViewModel>();
            var courseIds = GetListFromString(courseIdsStr);
            var groupIds = GetListFromString(groupIdsStr);

            if (groupIds.Any())
            {
                foreach (var groupId in groupIds)
                {
                    //var scheduleInfo = DataService
                    //    .GetScheduleInfoesForGroup(
                    //        new Faculty { Id = facultyId },
                    //        new Group { Id = groupId },
                    //        new StudyYear { Id = studyYearId },
                    //        semestr)
                    //    .Select(x => new ScheduleInfoViewModel(x));

                    //result.AddRange(scheduleInfo);
                }   
            }

            foreach (var courseId in courseIds)
            {
                var scheduleInfo = DataService
                    .GetScheduleInfoesForCourse(
                        new Faculty { Id = facultyId },
                        new Course { Id = courseId },
                        new StudyYear { Id = studyYearId },
                        semestr)
                    .Select(x => new ScheduleInfoViewModel(x));

                result.AddRange(scheduleInfo);
            }

            return new JsonNetResult(result);
        }
    }
}
