using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{
    public class GroupController : NewBaseController
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
