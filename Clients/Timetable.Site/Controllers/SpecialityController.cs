using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Controllers
{
    public class SpecialityController: NewBaseController
    {
        [HttpGet]
        public JsonNetResult GetForBranch(int branchId)
        {
            var specialities = DataService.GetSpecialities(branchId)
                .Select(x => new SpecialityViewModel(x));

            return new JsonNetResult(specialities);
        }
    }
}