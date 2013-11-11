using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using Timetable.Site.NewDataService;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers.Api
{
    public class TutorialTypeController : BaseApiController
    {
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetTutorialTypes()
                .Select(x => new TutorialTypeViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(string name)
        {
            var tutorialType = new TutorialType { Name = name };
            NewDataService.Add(tutorialType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dTutorialType = new TutorialType {Id = id};
            NewDataService.Delete(dTutorialType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}