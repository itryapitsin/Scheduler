using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timetable.Site.DataService;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers
{
    public class GroupController : BaseController
    {
        [HttpGet]
        public ActionResult Get(int facultyId, string courseIdsStr)
        {
            var groups = new List<GroupViewModel>();
            var courseIds = GetListFromString(courseIdsStr);

            foreach (var courseId in courseIds)
            {
                var result = DataService
                    .GetGroupsForCourse(
                        new Faculty { Id = facultyId },
                        new Course { Id = courseId })
                    .Select(x => new GroupViewModel(x));

                groups.AddRange(result);
            }

            return new JsonNetResult(groups);
        }

    }
}
