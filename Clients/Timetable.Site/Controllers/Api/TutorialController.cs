using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class TutorialController : BaseApiController
    {
        public TutorialController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetForFaculty(int facultyId, int courseId)
        {
            var result = NewDataService
                .GetTutorialsForFaculty(facultyId, courseId)
                .Select(x => new TutorialViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(TutorialAddViewModel viewModel)
        {
            var tutorial = new Tutorial
            {
                Name = viewModel.Name,
                ShortName = viewModel.ShortName,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsActual = true,
            };

            NewDataService.Add(tutorial);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dTutorial = new Tutorial();
            dTutorial.Id = id;
            NewDataService.Delete(dTutorial);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
