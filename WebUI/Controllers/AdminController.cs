using System;
using System.Web.Mvc;
using BLL.DomainModel.Services;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserService userService;
        public AdminController(UserService userService)
        {
            this.userService = userService;
        }

        public ActionResult FindAllUsers()
        {
            var role = "User";
            var users = userService.FindUsersByRole(role);
            return View(users);
        }
        public void DeleteUser(Guid userId)
        {
            userService.DeleteUser(userId);
        }
    }
}
