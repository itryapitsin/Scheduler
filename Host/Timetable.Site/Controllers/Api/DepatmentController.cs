using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models;


namespace Timetable.Site.Controllers.Api
{
    public class DepartmentController : BaseApiController
    {
        public DepartmentController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetDeparmtents()
                .Select(x => new DepartmentViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
