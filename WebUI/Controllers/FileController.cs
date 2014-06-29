using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BLL.DomainModel.Services;
using System.Linq;

namespace WebUI.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class FileController : Controller
    {
        private readonly FileService fileService;
        private readonly UserService userService;
        private readonly string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["filePath"];

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
        [Authorize(Roles = "Admin")]
        public ActionResult GetUserFiles(Guid userId)
        {
            var files = fileService.FindFilesByOwnerId(userId);
            return View(files);
        }
        public ActionResult Download(string path, string name)
        {
            try
            {
               // throw new Exception();
                var fileBytes = fileService.Download(path, name);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
            }
            catch (Exception)
            {
                return View("Error", (object)Request.UrlReferrer.Segments[2]); 
            }

        }
        public void Delete(Guid id)
        {
            //throw new Exception();
            fileService.DeleteFile(id);
        }

        public bool IsFileExisting(string fileName)
        {
            return fileService.IsFileExisting(fileName, userService.FindUserByEmail(User.Identity.Name).Id);
        }

        [HttpGet]
        public ActionResult LoadFiles()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadFiles(HttpPostedFileBase file, /*string description,*/ bool isPublic)
        {

            try
            {
                //throw new Exception();
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var contentLength = file.ContentLength;
                    //var contentType = file.ContentType;

                    var data = new byte[] { };
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        data = binaryReader.ReadBytes(file.ContentLength);
                    }
                    var newFile = fileService.CreateFileEntity(data, fileName, isPublic,
                                                               userService.FindUserByEmail(User.Identity.Name).Id,
                                                               contentLength,
                                                               filePath + User.Identity.Name);
                    fileService.SaveFile(newFile);

                }
                return RedirectToAction("GetMyFiles");
            }
            catch (Exception)
            {
                return View("Error", (object)"LoadFiles");
            }

        }

        [AllowAnonymous]
        public ActionResult FindPublicFilesByName(string fileName)
        {
            var files = fileService.FindPublicFilesByName(fileName);
            return PartialView("PublicFiles/Table/Table", files);
        }

        public ActionResult FindMyFilesByName(string fileName)
        {
            var files = fileService.FindFilesByNameAndOwnerId(fileName,
                userService.FindUserByEmail(User.Identity.Name).Id);
            return PartialView("MyFiles/Table/Table", files);
        }

        public ActionResult FindMyFilesForAutoCompl(string fileName)
        {
            var files = fileService.FindFilesByNameAndOwnerId(fileName,
                userService.FindUserByEmail(User.Identity.Name).Id);
            return Json(files.Select(it => it.Name), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult FindFilesForAutoCompl(string fileName)
        {
            var files = fileService.FindPublicFilesByName(fileName);
            return Json(files.Select(it => it.Name), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindAllFiles()
        {
            var files = fileService.FindAllFiles();
            return View(files);
        }
    }
}
