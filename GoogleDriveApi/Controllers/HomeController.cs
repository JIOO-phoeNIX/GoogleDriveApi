using System;
using System.Web;
using System.Web.Mvc;
using GoogleDriveApi.Repository;

namespace GoogleDriveApi.Controllers
{    
    public class HomeController : Controller
    {
        /// <summary>
        /// This is the action method that is executed each time the user runs the application
        /// </summary>
        /// <returns>The Index.cshtml View</returns>
        public ActionResult Index()
        {           
            return View();
        }

        /// <summary>
        /// This action method handles the request to upload the file to Google Drive
        /// </summary>
        /// <param name="file">The file picked by the user</param>
        /// <returns>A message, describing the output of the Upload</returns>
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            //check if user picks a file
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    //try uploading the file to Google Drive and return appropriate message       
                    GoogleApiDriveRepository.UploadFile(file);
                }               
                catch (Exception)
                {                    
                    //upload failed, return failure message
                    return RedirectToAction("Index", new { status = "failure" });
                } 
                
                //upload successful, return success message
                return RedirectToAction("Index", new { status = "success" });
            }          
            else
            {
                //user doesn't pick a file, return nofile message
                return RedirectToAction("Index", new { status = "nofile" });
            }
        }
    }
}