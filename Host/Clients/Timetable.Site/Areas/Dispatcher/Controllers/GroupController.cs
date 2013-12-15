using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class GroupController : AuthorizedController
    {
        [HttpGet]
        public ActionResult Get(int facultyId, int courseId, int studyTypeId)
        {
            var result = DataService
                    .GetGroupsForFaculty(facultyId, courseId, studyTypeId)
                    .Select(x => new GroupViewModel(x));

            return new JsonNetResult(result);
        }

    }
}
