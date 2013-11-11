using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.DataService;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers.Api
{
    public class BranchController : BaseApiController<Branch>
    {
        //Получить список всех подразделений
        public HttpResponseMessage GetAll()
        {
            var branches = NewDataService.GetBranches()
                .Select(x => new BranchViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, branches);
        }
    }
}
