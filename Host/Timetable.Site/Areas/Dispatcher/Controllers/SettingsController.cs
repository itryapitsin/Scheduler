using System.Web.Mvc;
using Timetable.Data.Models.Personalization;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;

namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class SettingsController : AuthorizedController
    {
        public ActionResult Index()
        {
            var model = new SettingsViewModel
            {
                Login = UserData.Login,
                Firstname = UserData.Firstname,
                Middlename = UserData.Middlename,
                Lastname = UserData.Lastname
            };

            return View("_Index", model);
        }

        public ActionResult Save(SaveSettingsRequest request)
        {
            UserData.Firstname = request.Firstname;
            UserData.Middlename = request.Middlename;
            UserData.Lastname = request.Lastname;

            if(request.IsNeedPasswordUpdate())
                UserService.SaveUserState(UserData, request.Password);
            else
                UserService.SaveUserState(UserData);

            return new JsonNetResult(new SuccessResponse());
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
