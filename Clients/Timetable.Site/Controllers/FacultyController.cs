using System.Linq;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers
{
    public class FacultyController : NewBaseController
    {
        public JsonNetResult Get(int branchId)
        {
            var faculties = DataService
                .GetFaculties(new BranchDataTransfer {Id = branchId})
                .Select(x => new FacultyViewModel(x));

            return new JsonNetResult(faculties);
        }

    }
}
