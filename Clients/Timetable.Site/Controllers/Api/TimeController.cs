using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class TimeController : BaseApiController
    {
        public TimeController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage Get(int buildingId)
        {
            var times = NewDataService
                .GetTimes(buildingId)
                .Select(x => new TimeViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, times);
        }
    }
}