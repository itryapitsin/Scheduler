using System;
using System.Web.Mvc;
using System.Web.Security;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{

    public class SignInController : BaseController
    {
        public ActionResult Index(string returnUrl)
        {
            return View();
            
        }

        //Обработчик входа
        [HttpPost]
        public bool Index(SignInModel model)
        {
            if (ModelState.IsValid)
            {
               // bool f = UserService.ValidateUser(model.UserName, model.Password);

               if (model.UserName == "admin" && model.Password == "admin")
               {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "Имя пользователя или пароль некорректны");
                }
            }
           return false;
        }
         #region Private methods

        void RegistrationService_OnSuccess(object sender, EventArgs e)
        {
            Response.Redirect(Request["ReturnUrl"]);
        }

        void RegistrationService_OnFail(object sender, EventArgs e)
        {
            ModelState.AddModelError("Error", (sender as Exception).Message);
        }

        void AuthenticateService_OnFail(object sender, EventArgs e)
        {
            ModelState.AddModelError("Error", @Resources.Message.AuthorizeFailed);
        }

        #endregion
    }
}
