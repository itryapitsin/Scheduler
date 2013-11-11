using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Timetable.Site.Models.Groups;
using Course = Timetable.Site.DataService.Course;
using Faculty = Timetable.Site.DataService.Faculty;
using Group = Timetable.Site.DataService.Group;
using GroupViewModel = Timetable.Site.Models.Groups.GroupViewModel;
using Speciality = Timetable.Site.DataService.Speciality;

namespace Timetable.Site.Controllers.Api
{
    public partial class GroupController : BaseApiController<Group>
    {
        //Получить все группы, чей код попадает под маску 
        public HttpResponseMessage GetByCode(string code)
        {
            var result = NewDataService
                .GetGroupsByCode(code, 10)
                .Select(x => new GroupViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        //Получить все группы по факультету, курсу и специальности (обзательно должен быть выбран факультет и курс, 
        //специальность может не выбираться
        public HttpResponseMessage GetAll(int facultyId, string courseIds, string specialityIds)
        {
            throw new NotImplementedException();
        }

        //Получить группы по специальностям
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

        //Получить группы по курсам
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

        public IEnumerable<GroupViewModel> GetByIds(string groupIds)
        {
            var result = new List<GroupViewModel>();

            if (groupIds != null)
            {
                foreach (var groupId in groupIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (groupId != " ")
                    {
                        var tmp = DataService.GetGroupById(int.Parse(groupId));
                        //var tmp = GetTempGroupById(int.Parse(groupId));
                        result.Add(new GroupViewModel(tmp));
                    }
                }
            }

            return result;
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
                UpdateDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            DataService.Add(aGroup);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            var dGroup = new Group {Id = id};
            DataService.Delete(dGroup);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}