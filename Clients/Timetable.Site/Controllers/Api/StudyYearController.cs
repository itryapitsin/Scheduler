using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class StudyYearController : BaseApiController
    {
        public StudyYearController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetStudyYears()
                .Select(x => new StudyYearViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}