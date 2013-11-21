using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models;
using Timetable.Logic.Services;

namespace Timetable.Site.Controllers
{
    [Authorize]
    public class NewBaseController : Controller
    {
        protected readonly IDataService DataService;
        protected readonly UserService UserService;
        protected UserDataTransfer UserData { get; private set; }

        public NewBaseController()
        {
            DataService = new SchedulerService();
            UserService = new UserService();
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            UserData = UserService.FindUser(User.Identity.Name);
        }

        protected List<int> GetListFromString(string str)
        {
            if(str == "undefined")
                return new List<int>();

            var result = new List<int>();
            if (!String.IsNullOrEmpty(str))
                result = str
                    .Replace("[", "")
                    .Replace("]", "")
                    .Split(new [] {"," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
            return result;
        }
    }
}
