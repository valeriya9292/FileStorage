using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BLL.DomainModel.Services;

namespace WebUI.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly FileService fileService;
        private readonly UserService userService;

        public FileController(FileService fileService, UserService userService)
        {
            this.fileService = fileService;
            this.userService = userService;
        }
        [AllowAnonymous]
        public ActionResult GetPublicFiles()
        {
            var files = fileService.FindPublicFiles();
            return View(files);
        }
        public ActionResult GetMyFiles()
        {
            var files = fileService.FindFilesByOwnerId(userService.FindUserByEmail(User.Identity.Name).Id);//BAD!!!!
            return View(files);
        }
        public ActionResult Download(string path, string name)
        {
            var fileBytes = fileService.Download(path, name);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        [HttpGet]
        public ActionResult LoadFiles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoadFiles(HttpPostedFileBase file, string description, bool? isPublic)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var contentLength = file.ContentLength;
               // var contentType = file.ContentType;

                var data = new byte[] { };
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    data = binaryReader.ReadBytes(file.ContentLength);
                }
                var newFile = fileService.CreateFileEntity(data, fileName, isPublic ?? false,
                                             userService.FindUserByEmail(User.Identity.Name).Id, contentLength);
                fileService.SaveFile(newFile);

            }
            return View();
        }

        public ActionResult FindPublicFilesByName(string fileName)
        {
            return Json(fileService.FindPublicFilesByName(fileName), JsonRequestBehavior.AllowGet);
        }
    }
}
