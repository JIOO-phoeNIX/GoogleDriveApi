using System;
using System.Web;
using System.Web.Mvc;
using GoogleDriveApi.Repository;

namespace GoogleDriveApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    GoogleApiDriveRepository.UploadFile(file);
                }               
                catch (Exception)
                {
                    return RedirectToAction("Index", new { status = "failure" });
                }  
                
                return RedirectToAction("Index", new { status = "success" });
            }
            else
            {
                return RedirectToAction("Index", new { status = "nofile" });
            }
        }
    }
}