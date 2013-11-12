using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{
    public class CourseController: NewBaseController
    {
        [HttpPost]
        public JsonNetResult Get()
        {
            var courses = DataService.GetCources();
            var result = courses.OrderBy(x => x.Name).Select(x => new CourseViewModel(x));
            return new JsonNetResult(result);
        }
    }
}