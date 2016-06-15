using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebPortal.DataModels;

namespace WebPortal.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //CreateUser
        //[HttpPost]
        //public async Task<ActionResult> Create(CreateModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Users user = new Users { UserName = model.UserName, Email = model.Email};
        //        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            AddErrorsFromResult(result);
        //        }
        //    }
        //    return View(model);
        //}

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            Users user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User not found." });
            }
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            Users user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password, string name)
        {
            Users user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }
            return View(user);
        }

        // Helpers

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
           


