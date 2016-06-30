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
    public class ValidateController : Controller
    {
        SampleIdentityDb db = new SampleIdentityDb();
        PatientUserEntities patientdb = new PatientUserEntities();
        EmployeeandDoctorEntities empdocdb = new EmployeeandDoctorEntities();

            
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("~/Views/Validate/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckModel model)
        {
            //Users user = new Users { UserName = model.UserName };
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    var usertype = Session["UserType"].ToString();

                    if (usertype == "Patient")
                    {
                        var patientregister = Session["patientregister"].ToString();
                        var query = (from p in patientdb.person
                                     join ph in patientdb.patient_hospital_usage
                                     on p.person_id equals ph.patient_id
                                     select new
                                     {
                                         ph.visible_patient_id,
                                         Bday = p.date_of_birth
                                     }).Where(a => a.visible_patient_id == patientregister).FirstOrDefault();

                        if (query.Bday == model.Birthday)
                        {
                            TempData["info"] = "Please set your account password.";
                            TempData.Keep("info");
                            return RedirectToAction("Patient", "Register");
                        }
                        else
                        {
                            successful = true;
                            ModelState.AddModelError("", "Birthday and Hospital Number did not match.");
                        }
                    }
                    else if (usertype == "Employee")
                    {
                        var empregister = Session["empregister"].ToString();
                        var query = (from e in empdocdb.employee
                                     join ei in empdocdb.employee_info_view
                                     on e.employee_id equals ei.person_id
                                     select new
                                     {
                                         e.employee_nr,
                                         Bday = ei.date_of_birth
                                     }).Where(a => a.employee_nr.ToString() == empregister).FirstOrDefault();

                        if (query.Bday == model.Birthday)
                        {
                            TempData["info"] = "Please set your account password.";
                            TempData.Keep("info");
                            return RedirectToAction("Employee", "Register");
                        }
                        else
                        {
                            successful = true;
                            ModelState.AddModelError("", "Birthday and Employee Number did not match.");
                        }
                    }

                    else if (usertype == "Doctor")
                    {
                        var docregister = Session["docregister"].ToString();
                        var query = (from e in empdocdb.employee
                                     join ei in empdocdb.employee_info_view
                                     on e.employee_id equals ei.person_id
                                     select new
                                     {
                                         e.employee_nr,
                                         Bday = ei.date_of_birth
                                     }).Where(a => a.employee_nr.ToString() == docregister).FirstOrDefault();

                        if (query.Bday == model.Birthday)
                        {
                            TempData["info"] = "Please set your account password.";
                            TempData.Keep("info");
                            return RedirectToAction("Doctor", "Register");
                        }
                        else
                        {
                            successful = true;
                            ModelState.AddModelError("", "Birthday and Employee Number did not match.");
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

        public ActionResult PasswordChecker()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordChecker(ResultModel model, string id)
        {
            var user = UserManager.Find(User.Identity.Name, model.password);
            model.IsValidated = false;
            if (user != null)
            {
                model.IsValidated = true;
                return RedirectToAction("Laboratory", "Results", new { fileid = id, isvalidated = model.IsValidated});
            }
            else
            {
                model.IsValidated = false;
                ModelState.AddModelError("", "Invalid Password");
            }

            return View(model);
        }

        //Helpers

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private UsersManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UsersManager>(); }
        }
    }
}