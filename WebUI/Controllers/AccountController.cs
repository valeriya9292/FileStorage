using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.DomainModel.Services;
using WebUI.Models;
using WebUI.Providers;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService service;
        public AccountController(UserService service)
        {
            this.service = service;
        }
        public ActionResult Hello()
        {
            return View(Request.RequestContext.HttpContext.User);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var provider = (CustomMembershipProvider) Membership.Provider;
                if (provider.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Hello", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel viewModel)
        {
            //if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            //{
            //    ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            //    return View(viewModel);
            //}

            var anyUser = service.FindAllUsers().Any(u => u.Email.Contains(viewModel.Email));
            if (anyUser)
            {
                ModelState.AddModelError("Email", "Пользователь с таким адресом уже зарегистрирован");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider) Membership.Provider)
                                                    .CreateUser(viewModel.Email, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Hello", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View(viewModel);
        }

    }
}
