using System.Linq;
using System.Web.Mvc;
using BLL.DomainModel.Services;

namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FileService service;
        public HomeController(FileService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            ViewBag.FileName = service.FindAllFiles().First().Name;
            return View();
        }

    }
}
