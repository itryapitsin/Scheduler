using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.ResponseModels
{
    public class SettingsUpdateResponse: SuccessResponse
    {
        public string UserName { get; set; }
    }
}