using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timetable.Site.Models.ViewModels;
using Timetable.Site.NewDataService;
using Faculty = Timetable.Site.DataService.Faculty;

namespace Timetable.Site.Controllers.Api
{
    public class FacultyController : BaseApiController<Faculty>
    {
        //TODO: Is need?
        //Получить список всех факультетов
        public HttpResponseMessage GetAll(int branchId)
        {
            var result = NewDataService
               .GetFaculties(new BranchDataTransfer{Id = branchId})
               .Select(x => new FacultyViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public HttpResponseMessage Get(int branchId)
        {
            var result = NewDataService
               .GetFaculties(new BranchDataTransfer { Id = branchId })
               .Select(x => new FacultyViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
