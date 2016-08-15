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

        [Authorize(Roles = "Doctor, Patient")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Doctor, Patient")]
        public ActionResult Laboratory(string fileid, bool? isvalidated)
        {
            ResultModel rmodel = new ResultModel();
            var currentuser = User.Identity.GetUserName().ToString();

            string samplecurrentuser = User.Identity.GetUserName().ToString();
            string path = Server.MapPath("~/PDFResults/");
            DirectoryInfo dir = new DirectoryInfo(path);
            int period = db.PortalRetention.Where(a => a.Module == "Results").Select(a => a.Retention_Period).First();
            DateTime minDate = DateTime.Today.AddDays(period);
            rmodel.PDFFile = dir.GetFiles("*.pdf*").Where(a => a.Name.Contains(samplecurrentuser) && (a.CreationTime > minDate)).OrderByDescending(a => a.CreationTime);
            rmodel.retentionperiod = period * -1;
            rmodel.fileid = fileid;
            rmodel.IsValidated = isvalidated;
            
            return View(rmodel);
        }

        [Authorize(Roles = "Doctor, Patient")]
        public ActionResult Radiology()
        {
            return View();
        }

        [Authorize(Roles = "Doctor, Patient")]
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