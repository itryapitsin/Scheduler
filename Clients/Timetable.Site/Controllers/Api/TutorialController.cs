using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using Timetable.Site.Models.Tutorials;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class TutorialController : BaseApiController
    {
        public HttpResponseMessage GetForFaculty(int facultyId, int courseId)
        {
            var result = NewDataService
                .GetTutorialsForCourse(facultyId, courseId)
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

            return Request.CreateResponse(HttpStatusCode.OK, tutorial);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteModel model)
        {
            var dTutorial = new Tutorial();
            dTutorial.Id = model.Id;
            NewDataService.Delete(dTutorial);
        }

        public void privateDelete(int Id)
        {
            var dTutorial = new Tutorial();
            dTutorial.Id = Id;
            NewDataService.Delete(dTutorial);
        }
    }
}
