using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.ServiceModel;
using Timetable.Site.NewDataService;
using Timetable.Site.UserService;

namespace Timetable.Site.Controllers
{
    public class NewBaseController : Controller
    {
        protected readonly IDataService DataService;

        public NewBaseController()
        {
            DataService = new DataServiceClient();
        }

        protected List<int> GetListFromString(string str)
        {
            var result = new List<int>();
            if (!String.IsNullOrEmpty(str))
                result = str.Split(new [] {"," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
            return result;
        }
    }
}
