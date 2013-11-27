using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers
{
    public class GroupController : AuthorizedController
    {
        [HttpGet]
        public ActionResult Get(int facultyId, int courseId)
        {
            var result = DataService
                    .GetGroupsForFaculty(facultyId,courseId)
                    .Select(x => new GroupViewModel(x));

            return new JsonNetResult(result);
        }

    }
}
