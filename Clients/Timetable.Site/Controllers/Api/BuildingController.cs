using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers.Api
{
    public class BuildingController : BaseApiController
    {
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetBuildings()
                .Select(x => new BuildingViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
