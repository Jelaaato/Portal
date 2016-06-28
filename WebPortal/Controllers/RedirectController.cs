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
        EmployeeandDoctorEntities empdocdb = new EmployeeandDoctorEntities();

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
                    var email = (from e in patientdb.email
                                 join ph in patientdb.patient_hospital_usage on e.person_id equals ph.patient_id
                                 select new
                                 {
                                     hn = ph.visible_patient_id,
                                     em = e.email_address
                                 }).Where(a => a.hn == model.UserName).FirstOrDefault();

                    if (ModelState.IsValid)
                    {
                        Users user = new Users { UserName = model.UserName };

                        if (db.Users.Any(a => a.UserName == model.UserName))
                        {
                            Session["patientlogin"] = model.UserName.ToString();
                            return RedirectToAction("Login", "Account");
                        }

                        else if ((patientdb.patient_hospital_usage.Any(a => a.visible_patient_id == user.UserName)) && (email != null))
                        {
                            TempData["UserType"] = "Patient";
                            Session["patientemail"] = email.em.ToString();
                            Session["patientregister"] = model.UserName.ToString();
                            return RedirectToAction("Index", "Validate");
                        }

                        else if ((patientdb.patient_hospital_usage.Any(a => a.visible_patient_id == user.UserName)) && (email == null))
                        {
                            successful = true;
                            ModelState.AddModelError("", "You don't have any email registered in Orion");
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

                        else if (empdocdb.employee.Any(a => a.employee_nr.ToString() == user.UserName))
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectDoc(CheckModel model)
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
                        var query = (from e in empdocdb.employee
                                     join ev in empdocdb.employment_nl_view on e.employee_id equals ev.employee_id
                                     join jt in empdocdb.job_type on ev.job_type_code equals jt.job_type_code
                                     select new
                                     {
                                         emp_nr = e.employee_nr,
                                         jcc = jt.job_category_code
                                     }).Where(a => a.jcc == "DOC" && a.emp_nr.ToString() == user.UserName).FirstOrDefault();

                        if (db.Users.Any(a => a.UserName == model.UserName))
                        {
                            Session["doclogin"] = model.UserName.ToString();
                            return RedirectToAction("Login", "Account");
                        }

                        else if (query != null)
                        {
                            if (empdocdb.employee.Any(a => a.employee_nr == query.emp_nr))
                            {
                                TempData["UserType"] = "Doctor";
                                Session["docregister"] = model.UserName.ToString();
                                return RedirectToAction("Index", "Validate");
                            }
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