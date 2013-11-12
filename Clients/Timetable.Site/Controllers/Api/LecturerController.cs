using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class LecturerController : BaseApiController
    {
        public LecturerController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetByDepartment(int departmentId)
        {
            var result = NewDataService
                .GetLecturersByDeparmentId(departmentId)
                .Select(x => new LecturerViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetByMask(string mask)
        {
            var result = new LecturerViewModel(
                NewDataService.GetLecturerByFirstMiddleLastname(mask));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(LecturerAddViewModel model)
        {
            var lecturer = model.ToLecturer();
            NewDataService.Add(lecturer);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dLecturer = new Lecturer {Id = id};
            NewDataService.Delete(dLecturer);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}