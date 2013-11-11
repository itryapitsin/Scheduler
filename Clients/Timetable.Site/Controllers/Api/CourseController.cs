using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.DataService;
using Timetable.Site.Models.Courses;
using System.Web.Http;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers.Api
{
    public class CourseController : BaseApiController<Course>
    {
        //Получить список курсов
        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetCources()
                .Select(x => new CourseViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public IEnumerable<SendModel> privateGetAll()
        {
            var result = new List<SendModel>();
            var tmp = DataService.GetCources();
            //var tmp = GetTempCourses();

            foreach (var t in tmp)
            {
                result.Add(new SendModel(t));
            }

            return result;
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

            DataService.Add(course);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteModel model)
        {
            var dCourse = new Course {Id = model.Id};
            DataService.Delete(dCourse);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}