using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.NewDataService;
using Timetable.Site.Models.AuditoriumTypes;
using System.Web.Http;

namespace Timetable.Site.Controllers.Api
{
    public class AuditoriumTypeController : BaseApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetAuditoriumTypes()
                .Select(x => new AuditoriumTypeViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(AddModel model)
        {
            var aAuditoriumType = new AuditoriumType
            {
                Name = model.Name,
                UpdateDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            NewDataService.Add(aAuditoriumType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteModel model)
        {
            var dAuditoriumType = new Auditorium {Id = model.Id};
            NewDataService.Delete(dAuditoriumType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}