using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models;


namespace Timetable.Site.Controllers.Api
{
    public class WeekTypeController : BaseApiController
    {
        public WeekTypeController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetWeekTypes()
                .Select(x => new WeekTypeViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}