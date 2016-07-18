using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using WebPortal.DataModels;
using System.IO;

namespace WebPortal.Controllers
{
    public class ResultsController : Controller
    {
        private RetentionEntities db = new RetentionEntities();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Laboratory(string fileid, bool? isvalidated)
        {
            ResultModel rmodel = new ResultModel();
            var currentuser = User.Identity.GetUserName().ToString();

            string samplecurrentuser = User.Identity.GetUserName().ToString();
            string path = Server.MapPath("~/PDFResults/");
            DirectoryInfo dir = new DirectoryInfo(path);
            int period = db.PortalRetention.Where(a => a.Module == "Results").Select(a => a.Retention_Period).First();
            DateTime minDate = DateTime.Today.AddDays(period);
            rmodel.PDFFile = dir.GetFiles("*.pdf*").Where(a => a.Name.Contains(samplecurrentuser) && (a.CreationTime > minDate));
            rmodel.retentionperiod = period * -1;
            rmodel.fileid = fileid;
            rmodel.IsValidated = isvalidated;
            
            return View(rmodel);
        }

        //[HttpPost]
        //public ActionResult Validate(ResultModel model, int fileID)
        //{
        //    var user = UserManager.Find(User.Identity.Name, model.Password);
        //    if (user != null)
        //    {
        //        model.IsValidated = true;
        //        if (model.IsValidated == true)
        //        {
        //            Session["Validated"] = "yes";
        //            Session["fileid"] = model.fileid;
        //        }
        //        else
        //        {
        //            Session["Validated"] = "no";
        //        }

        //        return RedirectToAction("Laboratory", "Results", model);
        //    }
        //    else
        //    {
        //        model.IsValidated = false;

        //        return Redirect("/Results/Laboratory");
        //    }

        //}

        //[Authorize]
        //public ActionResult UpdateRetention()
        //{
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult UpdateRetention(ResultModel model)
        //{
        //    if (db.PortalResultsRetention.Any(a => a.UserName == HttpContext.User.Identity.Name))
        //    {
        //        if ((model.retentionperiod <= 0) || (model.retentionperiod > 30))
        //        {
        //            @ViewBag.Error = "Please enter a value greater than 0 and less than 30 days.";
        //        }
        //        else
        //        {
        //            PortalResultsRetention user = db.PortalResultsRetention.First(a => a.UserName == HttpContext.User.Identity.Name);
        //            user.Retention_Period = model.retentionperiod * -1;
        //            db.SaveChanges();
        //            return RedirectToAction("Laboratory", "Results");
        //        }
        //    }
        //    else
        //    {
        //        if ((model.retentionperiod <= 0) || (model.retentionperiod > 30))
        //        {
        //            @ViewBag.Error = "Please enter a value greater than 0 and less than 30 days.";
        //        }
        //        else
        //        {
        //            int period = model.retentionperiod * -1;
        //            PortalResultsRetention retention = new PortalResultsRetention()
        //            {
        //                User_Id = HttpContext.User.Identity.GetUserId(),
        //                UserName = HttpContext.User.Identity.Name,
        //                Retention_Period = period,
        //            };

        //            db.PortalResultsRetention.Add(retention);
        //            db.SaveChanges();
        //            return RedirectToAction("Laboratory", "Results");
        //        }
       
        //    }
        //    return View(model);
            
        //}

        [Authorize]
        public ActionResult Radiology()
        {
            return View();
        }

        [Authorize]
        public ActionResult Diagnosis()
        {
            return View();
        }
        
        private UsersManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UsersManager>(); }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}