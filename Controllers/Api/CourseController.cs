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
    public class CourseController : BaseApiController
    {
        //Получить список курсов
        public CourseController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetAll()
        {
            var result = NewDataService
                .GetCources()
                .Select(x => new CourseViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Add(string name)
        {
            var course = new Course
            {
                Name = name,
                UpdatedDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            NewDataService.Add(course);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dCourse = new Course {Id = id};
            NewDataService.Delete(dCourse);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}