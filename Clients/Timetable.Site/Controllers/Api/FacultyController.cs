﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class FacultyController : BaseApiController
    {
        public FacultyController(IDataService dataService) : base(dataService)
        {
        }

        [HttpGet]
        public HttpResponseMessage Get(int branchId)
        {
            var result = NewDataService
               .GetFaculties(new BranchDataTransfer { Id = branchId })
               .Select(x => new FacultyViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
