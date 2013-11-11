using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.NewDataService;
using Timetable.Site.Models.Courses;
using System.Web.Http;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers.Api
{
    public class CourseController : BaseApiController
    {
        //Получить список курсов
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetCources()
                .Select(x => new CourseViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(CourseAddViewModel viewModel)
        {
            var course = new Course
            {
                Name = viewModel.Name,
                UpdateDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            NewDataService.Add(course);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteModel model)
        {
            var dCourse = new Course {Id = model.Id};
            NewDataService.Delete(dCourse);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}