using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using BLL.DomainModel.Services;
using WebUI.Infrastructure;
using WebUI.Models;
using WebUI.Providers;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserService service;
        public AccountController(UserService service)
        {
            this.service = service;
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
                var provider = (CustomMembershipProvider)Membership.Provider;
                if (provider.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    var roleProvider = new CustomRoleProvider();
                    return roleProvider.IsUserInRole(viewModel.Email, "User") ? RedirectToAction("GetMyFiles", "File") 
                        : RedirectToAction("FindAllUsers", "Admin");
                }
                ModelState.AddModelError("", "Incorrect email or password");
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
            if (viewModel.Captcha != (string)Session[Infrastructure.Captcha.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Text from picture is not correct");
                return View(viewModel);
            }

            var anyUser = service.FindAllUsers().Any(u => u.Email.Contains(viewModel.Email));
            if (anyUser)
            {
                ModelState.AddModelError("Email", "User with this address already exists");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                                                    .CreateUser(viewModel.Email, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("GetPublicFiles", "File");
                }
                ModelState.AddModelError("", "Registration error");
            }
            return View(viewModel);
        }

        public ActionResult Captcha()
        {
            Session[Infrastructure.Captcha.CaptchaValueKey] = RandomUtil.GetRandomString(4);
            var captcha = new Captcha(Session[Infrastructure.Captcha.CaptchaValueKey].ToString(), 250, 100,
                FontFamily.Families.ElementAt(/*RandomUtil.GetRandomInt(FontFamily.Families.Length - 1)*/1).Name);
            Response.Clear();
            Response.ContentType = "image/jpeg";
            captcha.Image.Save(Response.OutputStream, ImageFormat.Jpeg);
            captcha.Dispose();
            return null;
        }
    }
}
