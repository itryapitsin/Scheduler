using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers.Api
{
    public class SpecialityController : BaseApiController
    {
        public SpecialityController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage Get(int branchId)
        {
            var result = NewDataService
                .GetSpecialities(branchId)
                .Select(x => new SpecialityViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
