using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Web.Http;
using Timetable.Site.DataService;
using Timetable.Site.Models.Faculties;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers.Api
{
    public partial class FacultyController : BaseApiController<Faculty>
    {
        //Получить список всех факультетов
        public HttpResponseMessage GetAll(int branchId)
        {
            return CreateResponse<int, IEnumerable<SendModel>>(privateGetAll, branchId);
        }

        [HttpGet]
        public HttpResponseMessage Get(int branchId)
        {
            var faculties = DataService
                .GetFaculties(new Branch {Id = branchId})
                .Select(x => new FacultyViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, faculties);
        }

        private IEnumerable<SendModel> privateGetAll(int branchId)
        {
            var result = new List<SendModel>();

            var qBranch = new Branch();
            qBranch.Id = branchId;

            var tmp = DataService.GetFaculties(qBranch);
            //var tmp = GetTempFaculties();

            foreach (var t in tmp)
            {
                result.Add(new SendModel(t));
            }

            return result;
        }
    }
}
