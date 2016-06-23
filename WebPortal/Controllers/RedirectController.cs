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

namespace WebPortal.Controllers
{
    public class RedirectController : Controller
    {
        SampleIdentityDb db = new SampleIdentityDb();
        PatientUserEntities patientdb = new PatientUserEntities();
        EmployeeUserEntities empdb = new EmployeeUserEntities();

        [AllowAnonymous]
        public ActionResult RedirectPat()
        {
            return View();
        }

        public ActionResult RedirectDoc()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RedirectEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectPat(CheckModel model)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Users user = new Users { UserName = model.UserName };

                        if (db.Users.Any(a => a.UserName == model.UserName))
                        {
                            Session["patientlogin"] = model.UserName.ToString();
                            return RedirectToAction("Login", "Account");
                        }

                        else if (patientdb.patient_hospital_usage.Any(a => a.visible_patient_id == user.UserName))
                        {
                            TempData["UserType"] = "Patient";
                            Session["patientregister"] = model.UserName.ToString();
                            return RedirectToAction("Index", "Validate");
                        }

                        else
                        {
                            successful = true;
                            ModelState.AddModelError("", "Invalid hospital number.");
                        }
                    }
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectEmp(CheckModel model)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Users user = new Users { UserName = model.UserName };

                        if (db.Users.Any(a => a.UserName == model.UserName))
                        {
                            Session["emplogin"] = model.UserName.ToString();
                            return RedirectToAction("Login", "Account");
                        }

                        else if (empdb.employee.Any(a => a.employee_nr.ToString() == user.UserName))
                        {
                            TempData["UserType"] = "Employee";
                            Session["empregister"] = model.UserName.ToString();
                            return RedirectToAction("Index", "Validate");
                        }

                        else
                        {
                            successful = true;
                            ModelState.AddModelError("", "Invalid employee number.");
                        }
                    }
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return View(model);
        }

       
        // Helpers

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