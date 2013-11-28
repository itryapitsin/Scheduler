using System.Linq;
using Timetable.Logic.Models.Scheduler;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class FacultyController : AuthorizedController
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
