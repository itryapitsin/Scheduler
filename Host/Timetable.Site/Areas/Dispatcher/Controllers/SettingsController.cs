using System.Web.Mvc;
using Timetable.Data.Models.Personalization;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Infrastructure;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SettingsController : AuthorizedController
    {
        public ActionResult Index()
        {
            return View("_Index");
        }

        public ActionResult CreateEditUser(CreateEditUserRequest request)
        {
            if(request.Password != request.ConfirmPassword)
                return new JsonNetResult(new FailResponse("Пароли не совпадают"));

            UserService.CreateUser(request.Login, request.Password, UserRoleType.Dispatcher);

            return new JsonNetResult(new SuccessResponse());
        }
    }
}
