using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models;

using System.Web.Http;

namespace Timetable.Site.Controllers.Api
{
    public class AuditoriumTypeController : BaseApiController
    {
        public AuditoriumTypeController(IDataService dataService) : base(dataService)
        {
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetAuditoriumTypes()
                .Select(x => new AuditoriumTypeViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(string name)
        {
            var aAuditoriumType = new AuditoriumType
            {
                Name = name,
                UpdatedDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            NewDataService.Add(aAuditoriumType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dAuditoriumType = new Auditorium {Id = id};
            NewDataService.Delete(dAuditoriumType);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}