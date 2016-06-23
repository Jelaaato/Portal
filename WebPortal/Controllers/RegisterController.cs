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
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

namespace WebPortal.Controllers
{
    public class RegisterController : Controller
    {
        public SampleIdentityDb db = new SampleIdentityDb();
        public PatientUserEntities patientuser = new PatientUserEntities();

        //Register Patient
        [AllowAnonymous]
        public ActionResult RegisterPat()
        {
            //ViewBag.username = TempData["registeruser"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterPat(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users { UserName = model.UserName, Email = model.Email};
                if (db.Users.Any(a => a.Email == model.Email))
                {
                    ModelState.AddModelError("", "Email is already in use.");
                }
                else
                {
                    IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        Session["patientregister"] = model.UserName;
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Register", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirm your Account",
                            "Good Day!<br/><br/><p>Thank you for signing up.<br/><br/>Please <a href=\"" + callbackUrl + "\">confirm your account </a>to complete the registration process.</p><br/><br/>Thank you very much.");

                        var currentuser = UserManager.FindByName(user.UserName);
                        UserManager.AddToRole(currentuser.Id, "Patient");

                        return View("EmailSent");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                } 
             }
                return View(model);
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //Register Doctor
        public ActionResult RegisterDoc()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> RegisterDoc(CreateModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Users user = new Users { UserName = model.UserName };
        //        if (patientuser.User_Doctor.Any(a => a.employee_nr == user.UserName))
        //        {
        //            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        //            if (result.Succeeded)
        //            {
        //                TempData["registeruser"] = model.UserName.ToString();
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "No user found.");
        //            }
        //        }
        //    }
        //    return View(model);
        //}


        //Register Employee

        public ActionResult RegisterEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmp(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users { UserName = model.UserName, Email = model.Email};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Session["empregister"] = model.UserName;

                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Register", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your Account",
                        "Good Day!<br/><br/><p>Thank you for signing up.<br/><br/>Please <a href=\"" + callbackUrl + "\">confirm your account </a>to complete the registration process.</p><br/><br/>Thank you very much.");

                    var currentuser = UserManager.FindByName(user.UserName);
                    UserManager.AddToRole(currentuser.Id, "Employee");

                    return View("EmailSent");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }




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