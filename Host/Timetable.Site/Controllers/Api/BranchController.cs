using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models;


namespace Timetable.Site.Controllers.Api
{
    public class BranchController : BaseApiController
    {
        public BranchController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetAll()
        {
            var branches = NewDataService.GetBranches()
                .Select(x => new BranchViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, branches);
        }
    }
}
