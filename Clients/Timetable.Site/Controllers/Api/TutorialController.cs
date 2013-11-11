using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using Timetable.Site.DataService;
using Timetable.Site.Models.Tutorials;

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
            return CreateResponse(privateAdd, viewModel);
        }

        public void privateAdd(TutorialAddViewModel viewModel)
        {
            var aTutorial = new Tutorial();

            var qFaculty = new Faculty();
            qFaculty.Id = viewModel.FacultyId;

            aTutorial.Faculty = qFaculty;

            var qSpeciality = new Speciality();
            qSpeciality.Id = viewModel.SpecialityId;

            aTutorial.Speciality = qSpeciality;

            aTutorial.Name = viewModel.Name;
            aTutorial.ShortName = viewModel.ShortName;

            aTutorial.UpdateDate = DateTime.Now.Date;
            aTutorial.CreatedDate = DateTime.Now.Date;
            aTutorial.IsActual = true;

            
            DataService.Add(aTutorial);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteModel model)
        {
            return CreateResponse(privateDelete, model.Id);
        }

        public void privateDelete(int Id)
        {
            var dTutorial = new Tutorial();
            dTutorial.Id = Id;
            DataService.Delete(dTutorial);
        }
    }
}
