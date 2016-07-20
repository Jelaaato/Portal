using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: loginform
        [OutputCache(Duration = 3600)]
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        
    }
}