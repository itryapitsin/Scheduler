using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{
    public class SpecialityController: NewBaseController
    {
        [HttpGet]
        public JsonNetResult GetForBranch(int branchId)
        {
            var specialities = DataService.GetSpecialities(branchId)
                .Select(x => new SpecialityViewModel(x));

            return new JsonNetResult(specialities);
        }
    }
}