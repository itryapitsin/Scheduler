using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Data.Models.Scheduler;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models;



namespace Timetable.Site.Controllers.Api
{
    public class GroupController : BaseApiController
    {
        public GroupController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetByCode(string code)
        {
            var result = NewDataService
                .GetGroupsByCode(code, 10)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetBySpeciality(int specialityId, string courseIds)
        {
            var ids = courseIds.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = NewDataService
                .GetGroupsForSpeciality(specialityId, ids)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetByCourses(int facultyId, string courseIds)
        {
            var ids = courseIds.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = NewDataService
                .GetGroupsForFaculty(facultyId, ids)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetByIds(string groupIds)
        {
            var ids = groupIds.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var result = NewDataService
                .GetGroupsByIds(ids)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage Add(GroupAddViewModel viewModel)
        {
            var aGroup = new Group
            {
                Code = viewModel.Code,
                StudentsCount = viewModel.StudentsCount,
                Course = new Course {Id = viewModel.CourseId},
                Parent = new Group {Id = viewModel.ParentId},
                Speciality = new Speciality {Id = viewModel.SpecialityId},
                UpdatedDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            NewDataService.Add(aGroup);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dGroup = new Group {Id = id};
            NewDataService.Delete(dGroup);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}