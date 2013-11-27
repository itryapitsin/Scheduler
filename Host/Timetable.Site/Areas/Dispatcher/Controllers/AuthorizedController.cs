using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models;
using Timetable.Logic.Services;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    [Authorize]
    public class AuthorizedController : BaseController
    {
        protected readonly IDataService DataService;
        
        protected UserDataTransfer UserData { get; private set; }

        public AuthorizedController()
        {
            DataService = new SchedulerService();
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!User.Identity.IsAuthenticated)
            {
                filterContext.Result = RedirectToAction("Login", "Auth");
                return;
            }

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
