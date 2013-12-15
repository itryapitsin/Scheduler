using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class SuccessResponse
    {
        public bool Ok { get; set; }
        public SuccessResponse()
        {
            Ok = true;
        }
    }
}