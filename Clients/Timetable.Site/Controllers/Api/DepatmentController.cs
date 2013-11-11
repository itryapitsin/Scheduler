using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.DataService;
using Timetable.Site.Models.Departments;

namespace Timetable.Site.Controllers.Api
{
    public class DepartmentController : BaseApiController<Department>
    {
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetDeparmtents()
                .Select(x => new DepartmentViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
