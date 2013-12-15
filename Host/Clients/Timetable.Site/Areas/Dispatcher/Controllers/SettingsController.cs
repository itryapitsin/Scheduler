using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Timetable.Data.Models.Personalization;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;
using Timetable.Site.Areas.Dispatcher.Models.ResponseModels;
using Timetable.Site.Areas.Dispatcher.Models.ViewModels;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

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
                Lastname = UserData.Lastname,
                Users = UserService
                    .GetUsers(new [] { UserData.Login })
                    .Select(x => new UserViewModel(x))
            };

            return View("_Index", model);
        }

        public ActionResult Save(SaveSettingsRequest request)
        {
            UserData.Firstname = request.Firstname;
            UserData.Middlename = request.Middlename;
            UserData.Lastname = request.Lastname;

            if (request.IsNeedPasswordUpdate())
            {
                if (request.Password != request.ConfirmPassword)
                    return new JsonNetResult(new FailResponse("Пароли не совпадают"));

                UserService.SaveUserState(UserData, request.Password);
            }
            else
                UserService.SaveUserState(UserData);

            return new JsonNetResult(new SettingsUpdateResponse{ UserName = UserData.GetUserName() });
        }

        public ActionResult CreateEditUser(CreateEditUserRequest request)
        {
            if(request.Password != request.ConfirmPassword)
                return new JsonNetResult(new FailResponse("Пароли не совпадают"));

            try
            {
                UserService.CreateUser(
                    request.Login,
                    request.Password,
                    request.Firstname,
                    request.Middlename,
                    request.Lastname,
                    UserRoleType.Dispatcher);
            }
            catch (DuplicateNameException ex)
            {
                return new JsonNetResult(new FailResponse("Этот логин уже занят"));
            }
            
            return new JsonNetResult(new SuccessResponse());
        }
    }
}
