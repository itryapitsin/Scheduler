using System.Web.Mvc;
using System.Web.Security;
using Timetable.Site.Models.RequestModels;

namespace Timetable.Site.Controllers
{
    public class AuthController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SignInRequest request)
        {
            if (ModelState.IsValid)
            {
                if (UserService.Validate(request.UserName, request.Password))
                {
                    FormsAuthentication.SetAuthCookie(request.UserName, request.RememberMe);
                    return RedirectToAction("Index", "Main");
                }
                ModelState.AddModelError("", "Имя пользователя или пароль некорректны");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}
