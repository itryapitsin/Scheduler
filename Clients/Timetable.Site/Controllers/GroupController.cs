using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers
{
    public class GroupController : NewBaseController
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
                        new FacultyDataTransfer { Id = facultyId },
                        new CourseDataTransfer { Id = courseId })
                    .Select(x => new GroupViewModel(x));

                groups.AddRange(result);
            }

            return new JsonNetResult(groups);
        }

    }
}
