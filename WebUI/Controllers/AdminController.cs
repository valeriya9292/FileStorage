using System.Web.Mvc;
using BLL.DomainModel.Services;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserService service;
        public AdminController(UserService service)
        {
            this.service = service;
        }

        public ActionResult FindAllUsers()
        {
            var users = service.FindAllUsers();
            return View(users);
        }

    }
}
