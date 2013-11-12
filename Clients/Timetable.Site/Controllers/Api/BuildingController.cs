using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class BuildingController : BaseApiController
    {
        public BuildingController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetBuildings()
                .Select(x => new BuildingViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
