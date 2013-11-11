using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using Timetable.Site.DataService;
using Timetable.Site.Models.ScheduleInfoes;

namespace Timetable.Site.Controllers.Api
{
    public partial class GroupController : BaseApiController<Group>
    {
        public Group CreateGroup(int Id, string Code, int specialityId, int courseId){
             var g1 = new Group();
             g1.Id = Id;
             g1.Code = Code;
             g1.Course = new Course();
             g1.Course.Id = courseId;
             g1.Speciality = new Speciality();
             g1.Speciality.Id = specialityId;

             return g1;
        }
    }



    
}